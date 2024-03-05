using GraduationProject.DTOs;
using GraduationProject.Helpers;
using GraduationProject.Models;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult> AddUser([FromForm]UserReq userReq)
        {
            try
            {
                User user = new User();
                user.Email = userReq.Email;
                user.UserName = userReq.User_Name;
                user.Role_Id = userReq.Role_Id;
                user.Password = userReq.Password;

                // Check Username
                if (await CheckUserNameExistAsync(user.UserName))
                    return BadRequest(new { Message = "Username Already Exists" });

                // Check Email
                if (await CheckEmailExistAsync(user.Email))
                    return BadRequest(new { Message = "Email Already Exists" });

                user.Password = PasswordHasher.HashPassword(user.Password);
                user.Token = "";
                _context.Users.Add(user);
                await _context.SaveChangesAsync();        
                return Ok();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        private Task<bool> CheckUserNameExistAsync(string username)
        => _context.Users.AnyAsync(x => x.UserName == username);

        private Task<bool> CheckEmailExistAsync(string email)
        => _context.Users.AnyAsync(x => x.Email == email);

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

        [Authorize]
        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<User>> GetAllUsers()
        {
            return Ok(await _context.Users.ToListAsync());
        }
    }
}
