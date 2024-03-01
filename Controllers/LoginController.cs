using GraduationProject.DTOs;
using GraduationProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public ActionResult UserLogin([FromForm] LoginReq loginReq)
        {
            try
            {
                User user = _context.Users
                                .FirstOrDefault(u => (u.Email==loginReq.UserName_Email||u.UserName==loginReq.UserName_Email) && u.Password == loginReq.Password);

                if (user == null)
                {
                    return NotFound("User not found");
                }

                
             return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
