using GraduationProject.DTOs;
using GraduationProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [HttpPost]
        public ActionResult AddUser([FromForm]UserReq userReq)
        {
            try
            {
                User user = new User();
                user.Email = userReq.Email;
                user.UserName = userReq.User_Name;
                user.Role_Id = userReq.Role_Id;
                user.Password = userReq.Password;

                _context.Users.Add(user);
                _context.SaveChanges();        
                return Ok();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetRolesDropdown")]
        public async Task<ActionResult> GetRolesDropdown() 
        {
            var roles = await _context.Roles.ToListAsync();
            
            List<RoleDropdownResponse> rolesDropdown = new List<RoleDropdownResponse>();
            foreach (var role in roles)
            {
                rolesDropdown.Add(new RoleDropdownResponse
                {
                    id = role.Id,
                    name = role.Name,
                }); 
            }
            return Ok(rolesDropdown);
        }
    }
}
