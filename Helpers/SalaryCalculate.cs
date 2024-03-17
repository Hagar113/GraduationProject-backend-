using GraduationProject.DTOs;
using GraduationProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Helpers
{
    public class SalaryCalculate
    {
        private readonly ApplicationDbContext _context;
        public SalaryCalculate(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public SalaryResponseDto CalcSalary(int id)
        {
            var Emp = _context.Employees
                               .Include(h => h.dept)
                               .Include(h => h.salary)
            .FirstOrDefault(h => h.Id == id);

            var settings = _context.generalSettings.OrderByDescending(s => s.Id).FirstOrDefault();

            var firstWeekDay = APIsHelper.GetNumberOfWeekdaysInMonth(settings != null && settings.SelectedFirstWeekendDay != null ? settings.SelectedFirstWeekendDay : "");
            var secondWeekDay = APIsHelper.GetNumberOfWeekdaysInMonth(settings != null && settings.SelectedSecondWeekendDay != null ? settings.SelectedSecondWeekendDay : "");

            int firstWeekDaysCount = (int)(firstWeekDay != null ? firstWeekDay : 0);
            int secondWeekDaysCount = (int)(secondWeekDay != null ? secondWeekDay : 0);

            var holidays = _context.Holidays.Where(h => h.Date.Month == DateTime.Now.Month).ToList();

            int HolidaysCount = holidays != null ? holidays.Count() : 0;

            int daysInCurrentMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

            int totalOfficialDaysInThisMonth = daysInCurrentMonth - firstWeekDaysCount - secondWeekDaysCount - HolidaysCount;

            double DayPrice = Emp.salary.NetSalary / 30;

            DateTime leaveTime = DateTime.Parse(Emp.LeaveTime);
            DateTime attendanceTime = DateTime.Parse(Emp.AttendanceTime);
            double timeDifferenceInHours = (leaveTime - attendanceTime).TotalHours;

            double HourPrice = DayPrice / timeDifferenceInHours;


            var attendances = _context.EmployeeAttendances.Where(z => z.EmployeeId == id && z.Attendence.Month == DateTime.Now.Month).ToList();

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

            if (settings.Method == "hour")
            {
                extraHours *= extraHoursAdjustment;
                lossHours *= discountHoursAdjustment;
            }
            SalaryResponseDto responseDto = new SalaryResponseDto();
            responseDto.empName = Emp.Name;
            responseDto.NetSalary = Emp.salary.NetSalary;
            responseDto.deptName = Emp.dept.Name;
            responseDto.attendanceDays = attendances != null ? attendances.Count() : 0;
            responseDto.absenceDays = totalOfficialDaysInThisMonth - (attendances != null ? attendances.Count() : 0);
            responseDto.exrtaHours = extraHours;
            responseDto.discountHours = lossHours;
            responseDto.extraSalary = (double)(settings.Method == "hour" ? (extraHours * HourPrice) : (extraHours * settings.Addition));
            responseDto.discountSalary = (double)(settings.Method == "hour" ? (lossHours * HourPrice) : (lossHours * settings.Deduction));


            double totalSalaey = responseDto.NetSalary + responseDto.extraSalary + responseDto.discountSalary;

            responseDto.totalSalary = totalSalaey - (DayPrice * (30 - attendances.Count()));

            return responseDto;
        }
    }
}
