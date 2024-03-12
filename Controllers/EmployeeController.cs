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
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllEmployees")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllEmployees()
        {
            try
            {
                List<EmployeeDto> employeeDtoList = await _context.Employees
                    .Select(employee => new EmployeeDto
                    {
                        Id = employee.Id,
                        Name = employee.Name,
                        Address = employee.Address,
                        Phone = employee.phone,
                        Gender = employee.gender.GName,
                        Nationality = employee.Nationality,
                        NationalId = employee.NationalId,
                        BirthDate = employee.Birthdate,
                        Contractdate = employee.Contractdate,
                        NetSalary = employee.salary.NetSalary,
                        AttendanceTime = employee.AttendanceTime,
                        LeaveTime = employee.LeaveTime
                    })
                    .ToListAsync();

                if (employeeDtoList.Count > 0)
                    return Ok(employeeDtoList);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpGet("emp/{empId}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeById(int empId)
        {
            try
            {
                EmployeeDto? employeeDto = await _context.Employees
                    .Where(employee => employee.Id == empId)
                    .Select(employee => new EmployeeDto
                    {
                        Id = employee.Id,
                        Name = employee.Name,
                        Address = employee.Address,
                        Phone = employee.phone,
                        GenderId = employee.gender.Id,
                        Gender = employee.gender.GName,
                        Nationality = employee.Nationality,
                        NationalId = employee.NationalId,
                        BirthDate = employee.Birthdate,
                        Contractdate = employee.Contractdate,
                        NetSalary = employee.salary.NetSalary,
                        AttendanceTime = employee.AttendanceTime,
                        LeaveTime = employee.LeaveTime
                    })
                    .FirstOrDefaultAsync();

                if (employeeDto != null)
                {
                    return Ok(employeeDto);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }


    }
}
