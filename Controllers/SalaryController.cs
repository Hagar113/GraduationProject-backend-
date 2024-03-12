using GraduationProject.DTOs;
using GraduationProject.Helpers;
using GraduationProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public SalaryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetALLEmpsSalaries")]
        public IActionResult GetALLEmpsSalaries()
        {
            var emps = _context.Employees.ToList();
            List<SalaryResponseDto> salaryResponseDtos = new List<SalaryResponseDto>();

            SalaryCalculate salary = new SalaryCalculate(_context);

            foreach (var emp in emps)
            {
                salaryResponseDtos.Add(salary.CalcSalary(emp.Id));
            }
            return Ok(salaryResponseDtos);
        }


        [HttpGet("{id}")]
        public IActionResult GetEmployeSalary(int id)
        {

            var Emp = _context.Employees
                                .Include(h => h.dept)
                                .Include(h => h.salary)
                                .FirstOrDefault(h => h.Id == id);

            var settings = _context.generalSettings.FirstOrDefault();

            var firstWeekDay = APIsHelper.GetNumberOfWeekdaysInMonth(settings != null && settings.SelectedFirstWeekendDay != null ? settings.SelectedFirstWeekendDay : "");
            var secondWeekDay = APIsHelper.GetNumberOfWeekdaysInMonth(settings != null && settings.SelectedSecondWeekendDay != null ? settings.SelectedSecondWeekendDay : "");

            int firstWeekDaysCount = (int)(firstWeekDay != null ? firstWeekDay : 0);
            int secondWeekDaysCount = (int)(secondWeekDay != null ? secondWeekDay : 0);

            var holidays = _context.Holidays.Where(h => h.Date.Month == DateTime.Now.Month).ToList();

            int HolidaysCount = holidays != null ? holidays.Count() : 0;

            int daysInCurrentMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

            int totalOfficialDaysInThisMonth = daysInCurrentMonth - (firstWeekDaysCount + secondWeekDaysCount + HolidaysCount );


            double DayPrice = Emp.salary.NetSalary / 30;

            double timeDifferenceInHours = (Emp.LeaveTime - Emp.AttendanceTime).TotalHours;

            double HourPrice = DayPrice / timeDifferenceInHours;


            var attendances = _context.EmployeeAttendances.Where(z => z.EmployeeId == id && z.Attendence.Month == DateTime.Now.Month).ToList();



            int attendedDaysCount = attendances != null ? attendances.Count() : 0;

            int absenceDays = totalOfficialDaysInThisMonth - attendedDaysCount;

            double extraHours = 0;
            double lossHours = 0;

            foreach (var attendance in attendances)
            {
                var DifferenceInHours = (attendance.Departure - attendance.Attendence).TotalHours;
                var resetHours = DifferenceInHours - timeDifferenceInHours;
                if (resetHours > 0)
                {
                    extraHours += resetHours;
                }
                else
                {
                    lossHours += resetHours;
                }
            }

            SalaryResponseDto responseDto = new SalaryResponseDto();
            responseDto.empName = Emp.Name;
            responseDto.NetSalary = Emp.salary.NetSalary;
            responseDto.deptName = Emp.dept.Name;
            
            responseDto.attendanceDays = attendedDaysCount;

            responseDto.absenceDays = (totalOfficialDaysInThisMonth - attendedDaysCount);
            responseDto.exrtaHours = extraHours;
            responseDto.discountHours = lossHours;
            responseDto.extraSalary = (double)(extraHours * HourPrice * settings.Addition);
            responseDto.discountSalary = (double)(lossHours * HourPrice * settings.Deduction);
            
            double totalSalaey = responseDto.NetSalary + responseDto.extraSalary + responseDto.discountSalary;

             responseDto.totalSalary = totalSalaey - (DayPrice * (30 - attendedDaysCount));
            return Ok(responseDto);

        }
        //--------------------------h1---------------------------------------------

        private int CountWeekendsInMonth(int year, int month, List<string> selectedWeekendDays)
        {
            int weekendsCount = 0;
            int daysInMonth = DateTime.DaysInMonth(year, month);

            for (int i = 1; i <= daysInMonth; i++)
            {
                var currentDate = new DateTime(year, month, i);
                if (selectedWeekendDays.Contains(currentDate.DayOfWeek.ToString()))
                {
                    weekendsCount++;
                }
            }

            return weekendsCount;
        }

        private int CountWeekdaysInMonth(int year, int month, string? dayOfWeek)
        {
            int count = 0;
            int daysInMonth = DateTime.DaysInMonth(year, month);

            for (int i = 1; i <= daysInMonth; i++)
            {
                var currentDate = new DateTime(year, month, i);
                if (currentDate.DayOfWeek.ToString() == dayOfWeek)
                {
                    count++;
                }
            }

            return count;
        }

        [HttpGet("SearchEmployees")]
        public IActionResult GetSalaryReport(int month, int year)
        {
            var employees = _context.Employees
                .Include(e => e.dept)
                .Include(e => e.salary)
                .ToList();

            var settings = _context.generalSettings.OrderByDescending(s => s.Id).FirstOrDefault();

            if (settings == null)
            {
                return BadRequest("General settings not found.");
            }

            var generalSettingDTO = new GeneralSettingDTO
            {
                Deduction= settings.Deduction,
                Addition = settings.Addition,
                SelectedFirstWeekendDay = settings.SelectedFirstWeekendDay,
                SelectedSecondWeekendDay = settings.SelectedSecondWeekendDay,
            };

            var filteredSalaries = new List<SalaryResponseDto>();

            var employeesForMonthYear = employees.Where(e => _context.EmployeeAttendances.Any(a => a.EmployeeId == e.Id && a.Attendence.Month == month && a.Attendence.Year == year)).ToList();//IMP

            if (employeesForMonthYear.Count == 0)//IMP
            {
                return NotFound("No employees found for the selected month and year.");
            }

            foreach (var employee in employees)
            {
               

                var firstWeekDay = settings != null ? settings.SelectedFirstWeekendDay : null;
                var secondWeekDay = settings != null ? settings.SelectedSecondWeekendDay : null;

                int firstWeekDaysCount = !string.IsNullOrEmpty(firstWeekDay) ? CountWeekdaysInMonth(year, month, firstWeekDay) : 0;
                int secondWeekDaysCount = !string.IsNullOrEmpty(secondWeekDay) ? CountWeekdaysInMonth(year, month, secondWeekDay) : 0;

                //double DayPrice = employee.salary.NetSalary / DateTime.DaysInMonth(year, month);
                double DayPrice = employee.salary.NetSalary / 30;

                double timeDifferenceInHours = (employee.LeaveTime - employee.AttendanceTime).TotalHours;
                double HourPrice = timeDifferenceInHours > 0 ? DayPrice / timeDifferenceInHours : 0;

                var holidays = _context.Holidays.Where(h => h.Date.Month == month && h.Date.Year == year).ToList();
                int HolidaysCount = holidays?.Count() ?? 0;

                int daysInCurrentMonth = DateTime.DaysInMonth(year, month);

                //int daysInCurrentMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

                var selectedWeekendDays = new List<string> { generalSettingDTO.SelectedFirstWeekendDay, generalSettingDTO.SelectedSecondWeekendDay };
                int weekendsInMonth = CountWeekendsInMonth(year, month, selectedWeekendDays);
                //int weekendsInMonth = CountWeekendsInMonth(DateTime.Now.Year, DateTime.Now.Month, selectedWeekendDays);

                var selectedVacationDaysAsStrings = generalSettingDTO.SelectedVacationDays?.Select(day => ((DayOfWeek)day).ToString()).ToList() ?? new List<string>();//IMP

                int totalOfficialDaysInThisMonth = daysInCurrentMonth - (firstWeekDaysCount + secondWeekDaysCount + HolidaysCount);

                var attendances = _context.EmployeeAttendances//IMP
                    .Where(z => z.EmployeeId == employee.Id && z.Attendence.Month == month && z.Attendence.Year == year)
                    .ToList();

                int absenceDayss = totalOfficialDaysInThisMonth - attendances?.Count() ?? 0;

                var holidaysAndWeekends = holidays.Select(h => h.Date.DayOfWeek.ToString())
                    .Concat(selectedWeekendDays)
                    .Distinct();

                double extraHours = 0;
                double lossHours = 0;

                foreach (var attendance in attendances)
                {
                    var DifferenceInHours = (attendance.Departure - attendance.Attendence).TotalHours;
                    var resetHours = DifferenceInHours - timeDifferenceInHours;
                    if (resetHours > 0)
                    {
                        extraHours += resetHours;
                    }
                    else
                    {
                        lossHours += resetHours;
                    }
                }

                double extraHoursAdjustment = settings.Addition ?? 0;
                double discountHoursAdjustment = settings.Deduction ?? 0;

                extraHours *= extraHoursAdjustment;
                lossHours *= discountHoursAdjustment;

                var salary = new SalaryResponseDto
                {
                    empName = employee.Name,
                    NetSalary = employee.salary.NetSalary,
                    deptName = employee.dept.Name,
                    attendanceDays = attendances?.Count() ?? 0,
                    absenceDays = absenceDayss,
                    exrtaHours = extraHours,
                    discountHours = lossHours,
                    extraSalary = extraHours * HourPrice,
                    discountSalary = lossHours * HourPrice,
                    HourlyRate = HourPrice,
                    DailyRate = DayPrice,
                    WeekendDays = weekendsInMonth,
                    Month = month,
                    Year = year,
                };
                double totalSalarry = employee.salary.NetSalary + salary.extraSalary + salary.discountSalary;

                //salary.totalSalary = totalSalarry - (DayPrice * (totalOfficialDaysInThisMonth - attendances.Count()));

                salary.totalSalary = totalSalarry - (DayPrice * (30 - attendances.Count()));

                filteredSalaries.Add(salary);
            }

            return Ok(filteredSalaries);
        }


        // Helper method to calculate delay hours
        private double CalculateDelayHours(DateTime arrivalTime, DateTime scheduledArrivalTime)
        {
            if (arrivalTime > scheduledArrivalTime)
            {
                TimeSpan delay = arrivalTime - scheduledArrivalTime;
                return delay.TotalHours;
            }
            return 0;
        }

        // Helper method to calculate early arrival hours
        private double CalculateEarlyArrivalHours(DateTime arrivalTime, DateTime scheduledArrivalTime)
        {
            if (arrivalTime < scheduledArrivalTime)
            {
                TimeSpan earlyArrival = scheduledArrivalTime - arrivalTime;
                return earlyArrival.TotalHours;
            }
            return 0;
        }
        [HttpGet("GetEmployeeAttendanceDetails")]
        public IActionResult GetEmployeeAttendanceDetails(int employeeId, int month, int year)
        {
            var employee = _context.Employees
                .Include(e => e.dept)
                .Include(e => e.salary)
                .FirstOrDefault(e => e.Id == employeeId);

            if (employee == null)
            {
                return BadRequest("Employee not found or resigned.");
            }

            var settings = _context.generalSettings.OrderByDescending(s => s.Id).FirstOrDefault();

            if (settings == null)
            {
                return BadRequest("General settings not found.");
            }

            var generalSettingDTO = new GeneralSettingDTO
            {
                Deduction = settings.Deduction,
                Addition = settings.Addition,
                SelectedFirstWeekendDay = settings.SelectedFirstWeekendDay,
                SelectedSecondWeekendDay = settings.SelectedSecondWeekendDay,
            };

            var firstWeekDay = settings != null ? settings.SelectedFirstWeekendDay : null;
            var secondWeekDay = settings != null ? settings.SelectedSecondWeekendDay : null;

            int firstWeekDaysCount = !string.IsNullOrEmpty(firstWeekDay) ? CountWeekdaysInMonth(year, month, firstWeekDay) : 0;
            int secondWeekDaysCount = !string.IsNullOrEmpty(secondWeekDay) ? CountWeekdaysInMonth(year, month, secondWeekDay) : 0;

            double DayPrice = employee.salary.NetSalary / DateTime.DaysInMonth(year, month);

            var attendances = _context.EmployeeAttendances
                .Where(z => z.EmployeeId == employeeId && z.Attendence.Month == month && z.Attendence.Year == year)
                .OrderBy(a => a.Attendence)
                .ToList();

            var attendanceDetails = new List<AttendanceResponse>();

            foreach (var attendance in attendances)
            {
                var timeDifferenceInHours = (attendance.Departure - attendance.Attendence).TotalHours;
                var originalTimeDifferenceInHours = (employee.LeaveTime - employee.AttendanceTime).TotalHours;

                var extraHours = timeDifferenceInHours > originalTimeDifferenceInHours ? timeDifferenceInHours - originalTimeDifferenceInHours : 0;
                var earlyDepartureHours = originalTimeDifferenceInHours > timeDifferenceInHours ? originalTimeDifferenceInHours - timeDifferenceInHours : 0;

                var attendanceDetail = new AttendanceResponse
                {
                    id = attendance.Id,
                    name = employee.Name,
                    department = employee.dept.Name,
                    attend = attendance.Attendence.ToString("HH:mm"), // Format as "HH:mm" for time-only string
                    leave = attendance.Departure.ToString("HH:mm"),
                    date = attendance.Attendence.Date.ToString("yyyy-MM-dd"), // Format as "yyyy-MM-dd" for date-only string
                    OriginalAttend = employee.AttendanceTime.ToString("hh\\:mm"),
                    OriginalLeave = employee.LeaveTime.ToString("hh\\:mm"),

                    ExtraHours = extraHours,
                    EarlyDepartureHours = earlyDepartureHours,
                };

                attendanceDetails.Add(attendanceDetail);
            }

            return Ok(attendanceDetails);
        }




    }

}

