using GraduationProject.DTOs;
using GraduationProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AttendanceController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("GetAttendancies")]
        public IActionResult GetAttendancies([FromBody] GetAttendanceRequest attendanceRequest)
        {
            var attendacies2 = _context.EmployeeAttendances
                                    .Include(h => h.Employee)
                                        .ThenInclude(h => h.dept)
                                    .Where(h => h.Attendence >= attendanceRequest.from &&
                                                h.Departure <= attendanceRequest.to)
                                    .ToList();

            List<EmployeesAttendancesResponse> employeesAttendances = new List<EmployeesAttendancesResponse>();
            foreach (var attend in attendacies2)
            {
                employeesAttendances.Add(new EmployeesAttendancesResponse
                {
                    id = attend.Id,
                    empName = attend.Employee.Name,
                    deptName = attend.Employee.dept.Name,
                    day = attend.Attendence.ToShortDateString(),
                    attendance = attend.Attendence.ToShortTimeString(),
                    departure = attend.Departure.Value.ToShortTimeString(),
                });
            }
            return Ok(employeesAttendances);
        }


        [HttpGet("GetAllEmps")]
        public IActionResult GetAllEmps()
        {
            var attendacies = _context.EmployeeAttendances
                                .Include(h => h.Employee)
                                    .ThenInclude(h => h.dept)
                                .ToList();
            List<EmployeesAttendancesResponse> employeesAttendances = new List<EmployeesAttendancesResponse>();
            foreach (var attend in attendacies)
            {
                employeesAttendances.Add(new EmployeesAttendancesResponse
                {
                    id = attend.Id,
                    empName = attend.Employee.Name,
                    deptName = attend.Employee.dept.Name,
                    day = attend.Attendence.ToShortDateString(),
                    attendance = attend.Attendence.ToShortTimeString(),
                    departure = attend.Departure != null ? attend.Departure.Value.ToShortTimeString() : null,
                
                //departure = attend.Departure.Value.ToShortTimeString(),
            });
            }
            return Ok(employeesAttendances);
        }


        [HttpPost]
        public IActionResult AddEmpAttendace([FromBody] SaveEmpRequest request)
        {
            EmployeeAttendance employeeAttendance = new EmployeeAttendance();
            employeeAttendance.Attendence = request.attendance;
            //employeeAttendance.Departure = request.departure;
            employeeAttendance.EmployeeId = request.EmpId.Value;

            _context.EmployeeAttendances.Add(employeeAttendance);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public IActionResult EditEmpAttendace(SaveEmpRequest request)
        {
            var employeeAttendance = _context.EmployeeAttendances.Where(z => z.Id == request.id).FirstOrDefault();

            employeeAttendance.Departure = request.departure;
            employeeAttendance.Attendence = request.attendance;

            _context.Entry(employeeAttendance).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok();
        }


        [HttpDelete]
        public IActionResult SaveEmpAttendace(int id)
        {
            var empAttendance = _context.EmployeeAttendances.Find(id);
            _context.EmployeeAttendances.Remove(empAttendance);
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("GetAllEmployees")]
        public IActionResult GetAllEmployees()
        {
            var emps = _context.Employees.ToList();

            List<GetEmpsResponse> empsResponses = new List<GetEmpsResponse>();

            foreach (var emp in emps)
            {
                empsResponses.Add(new GetEmpsResponse { id = emp.Id, name = emp.Name });
            }

            return Ok(empsResponses);
        }


        [HttpGet("{id}")]
        public IActionResult GetAttendaceById(int id)
        {
            var attendance = _context.EmployeeAttendances.FirstOrDefault(h => h.Id == id);
            return Ok(attendance);
        }

    }
}