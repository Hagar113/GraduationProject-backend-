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

        [HttpGet]
        public IActionResult GetAttendancies(GetAttendanceRequest attendanceRequest)
        {
            //var attendacies = _context.EmployeeAttendances
            //                        .Where(h => h.Attendence.Day == DateTime.Now.Day && 
            //                                    h.Attendence.Month == DateTime.Now.Month && 
            //                                    h.Attendence.Year == DateTime.Now.Year)
            //                        .ToList();
            var attendacies2 = _context.EmployeeAttendances
                                    .Where(h => h.Attendence >= attendanceRequest.from &&
                                                h.Departure <= attendanceRequest.to)
                                    .ToList();
            return Ok(attendacies2);
        }

        [HttpPost]
        public IActionResult AddEmpAttendace(SaveEmpRequest request)
        {
            EmployeeAttendance employeeAttendance = new EmployeeAttendance();
            employeeAttendance.Attendence = request.attendance;
            employeeAttendance.Departure = request.departure;
            employeeAttendance.EmployeeId = request.EmpId;

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
            employeeAttendance.EmployeeId = request.EmpId;

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
                empsResponses.Add(new GetEmpsResponse { id = emp.Id , name = emp.Name });
            }

            return Ok(empsResponses);
        }

    }
}
