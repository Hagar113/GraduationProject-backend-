using GraduationProject.DTOs;
using GraduationProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeActionsController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        public EmployeeActionsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost("AddEmployee")]
        public async Task<ActionResult<EmployeeDto>> AddEmployee(EmployeeDto newEmployeeDto)
        {
            if (ModelState.IsValid)
            {
                bool isNationalIdUnique = await IsNationalIdUnique(newEmployeeDto.NationalId);

                if (!isNationalIdUnique)
                {
                    ModelState.AddModelError("NationalId", "The national ID must be unique.");
                    return BadRequest(ModelState);
                }
                Employee newEmployee = new Employee
                {
                    Id = newEmployeeDto.Id,
                    Name = newEmployeeDto.Name,
                    Address = newEmployeeDto.Address,
                    phone = newEmployeeDto.Phone,
                    Nationality = newEmployeeDto.Nationality,
                    NationalId = newEmployeeDto.NationalId,
                    Birthdate = newEmployeeDto.BirthDate,
                    Contractdate = newEmployeeDto.Contractdate,
                    gender = new Gender { GName = newEmployeeDto.Gender },
                    salary = new Salary { NetSalary = newEmployeeDto.NetSalary },
                    AttendanceTime = newEmployeeDto.AttendanceTime,
                    LeaveTime = newEmployeeDto.LeaveTime,


                };

                _context.Employees.Add(newEmployee);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetEmployeeById", "Employee", new { empId = newEmployee.Id }, newEmployee);

            }
            return BadRequest(ModelState);
        }
        private async Task<bool> IsNationalIdUnique(string nationalId)
        {
            return await _context.Employees.AllAsync(e => e.NationalId != nationalId);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> EditEmployee(int id, EmployeeDto updatedEmployeeDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Employee? employee = await _context.Employees.Include(e => e.gender).Include(e => e.salary)
                        .FirstOrDefaultAsync(x => x.Id == id);

                if (employee == null)
                {
                    return NotFound();
                }

                employee.Name = updatedEmployeeDto.Name;
                employee.Address = updatedEmployeeDto.Address;
                employee.phone = updatedEmployeeDto.Phone;
                employee.gender.GName = updatedEmployeeDto.Gender;
                employee.Nationality = updatedEmployeeDto.Nationality;
                employee.NationalId = updatedEmployeeDto.NationalId;
                employee.Birthdate = updatedEmployeeDto.BirthDate;
                employee.Contractdate = updatedEmployeeDto.Contractdate;
                employee.salary.NetSalary = updatedEmployeeDto.NetSalary;
                employee.AttendanceTime = updatedEmployeeDto.AttendanceTime;
                employee.LeaveTime = updatedEmployeeDto.LeaveTime;

                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpDelete("id")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            try
            {
                Employee? employee = await _context.Employees.Include(e => e.salary)

                                                             .Include(e => e.gender)
                                                             .FirstOrDefaultAsync(e => e.Id == id);

                if (employee == null)
                    return NotFound();


                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();

                return Ok(employee);
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }


    }
}

