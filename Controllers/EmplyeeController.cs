using GraduationProject.DTOs;
using GraduationProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace GraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmplyeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EmplyeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("saveEmployee")]
        public ActionResult saveEmployee([FromForm] EmpReq empReq)
        {
            if (empReq.Id <= 0)
            {
                // Add new Emp
                return AddEmp(empReq);
            }
            else
            {
                //Edit Emp
                return EditEmp(empReq);
            }
        }

        public ActionResult AddEmp([FromForm] EmpReq empReq)
        {
            try
            {
                Employee employee = new Employee();
                employee.phone = empReq.phone;
                employee.Address = empReq.Address;
                employee.Birthdate = empReq.Birthdate;
                employee.Contractdate = empReq.Contractdate;
                employee.deptid = empReq.deptid;
                employee.GId = empReq.GId;
                employee.NationalId = empReq.NationalId;
                employee.Nationality = empReq.Nationality;
                employee.Sal_ID = empReq.Sal_ID;
                employee.user_Id = empReq.user_Id;
                employee.Name = empReq.Name;
                employee.AttendanceTime = empReq.AttandaceTime;
                employee.LeaveTime = empReq.LeaveTime;
                _context.Employees.Add(employee);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public ActionResult EditEmp([FromForm] EmpReq empReq)
        {
            try
            {
                var employee = _context.Employees.Find(empReq.Id);
                employee.phone = empReq.phone;
                employee.Address = empReq.Address;
                employee.Birthdate = empReq.Birthdate;
                employee.Contractdate = empReq.Contractdate;
                employee.deptid = empReq.deptid;
                employee.GId = empReq.GId;
                employee.NationalId = empReq.NationalId;
                employee.Nationality = empReq.Nationality;
                employee.Sal_ID = empReq.Sal_ID;
                employee.user_Id = empReq.user_Id;
                employee.Name = empReq.Name;
                employee.AttendanceTime = empReq.AttandaceTime;
                employee.LeaveTime = empReq.LeaveTime;

                _context.Entry(employee).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult GetEmployeesList()
        {
            var employees = _context.Employees.ToList();
            return Ok(employees);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteEmployee(int id)
        {
            try
            {
                var Emp = _context.Employees.FirstOrDefault(z => z.Id == id);
                _context.Remove(Emp);
                _context.SaveChanges();
                return Ok();
            }
            catch { return NotFound(); }
        }   

        [HttpGet("{id}")]
        public ActionResult GetEmployeeById(int id)
        {
            try
            {
                var Emp = _context.Employees.Include(h => h.salary).FirstOrDefault(z => z.Id == id);
                GetEmpResponseDto responseDto = new GetEmpResponseDto();
                responseDto.Id = Emp.Id;
                responseDto.Name = Emp.Name;
                responseDto.phone = Emp.phone;
                responseDto.Nationality = Emp.Nationality;
                responseDto.Address = Emp.Address;
                responseDto.Birthdate = Emp.Birthdate;
                responseDto.Contractdate = Emp.Contractdate;
                responseDto.AttendanceTime = Emp.AttendanceTime;
                responseDto.LeaveTime = Emp.LeaveTime;
                responseDto.GId = Emp.GId;
                responseDto.NetSalary = Emp.salary.NetSalary;
                return Ok(Emp);
            }
            catch { return NotFound(); }
        }
        [HttpGet("GetGenderDropdown")]
        public ActionResult GetGenderDropdown()
        {
            var genders = _context.Genders.ToList();
            
            List<GenderDropdownResponse> genderDropdowns = new List<GenderDropdownResponse>();
            foreach (var gender in genders)
            {
                genderDropdowns.Add(new GenderDropdownResponse
                {
                    id= gender.Id,
                    name = gender.GName
                });
            }
            return Ok(genderDropdowns);
        }

    }
}
