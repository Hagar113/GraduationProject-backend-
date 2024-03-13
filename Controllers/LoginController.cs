using GraduationProject.DTOs;
using GraduationProject.Helpers;
using GraduationProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;

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
        //[HttpPost]
        //public ActionResult UserLogin([FromForm] LoginReq loginReq)
        //{
        //    try
        //    {
        //        User user = _context.Users
        //                        .FirstOrDefault(u => (u.Email==loginReq.UserName_Email||u.UserName==loginReq.UserName_Email) && u.Password == loginReq.Password);

        //        if (user == null)
        //        {
        //            return NotFound("User not found");
        //        }


        //     return Ok(user);
        //    }
        //    catch (Exception ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //}

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginReq loginDTO)
        {
            var user = await _context.Users.Include(u => u.role).FirstOrDefaultAsync(u => u.Email == loginDTO.Email);

            if (!PasswordHasher.VerifyPassword(loginDTO.Password, user.Password))
            {
                return BadRequest(new { Message = "Password is Incorrect" });
            }
            user.Token = CreateJwt(user);

            if (user == null)
            {
                return Unauthorized(); // Invalid credentials
            }

            // Save changes to the database
            await _context.SaveChangesAsync();

            var role = _context.Roles
                            .Include(h => h.RolesPermissions.Where(h => h.IsView))
                            .ThenInclude(h => h.page)
                            .Where(h => h.Id == user.Role_Id)
                            .FirstOrDefault();
            RoleResponse roleDTO = new RoleResponse();
            roleDTO.role_Id = role.Id;
            roleDTO.role_Name = role.Name;
            roleDTO.rolePermissionsDTOs = new List<RolePermissionsResponse>();
            List<RolePermissionsResponse> rolePermissions = new List<RolePermissionsResponse>();
            foreach (var permission in role.RolesPermissions)
            {
                rolePermissions.Add(new RolePermissionsResponse
                {
                    RolePermission_Id = permission.id,
                    page_Name = permission.page.name,
                    page_Id = permission.page.id,
                    icon = permission.page.icon,
                    label = permission.page.label,
                    routerLink = permission.page.routerLink,
                    activateRoute = permission.page.activeRoute,

                    isAdd = permission.IsAdd,
                    isEdit = permission.IsEdit,
                    isDelete = permission.IsDelete,
                    isView = permission.IsView,
                });
            }
            roleDTO.rolePermissionsDTOs = rolePermissions;
            return Ok(new { Token= user.Token, UserId = user.Id, Username = user.UserName, Email= user.Email , Role = roleDTO });
        }


        //private string CreateJwt(User user)
        //{
        //    if (user == null)
        //    {
        //        // Handle the case where the user is null
        //        throw new ArgumentNullException(nameof(user), "User cannot be null.");
        //    }

        //    var jwtTokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes("veryverysceret.....");
        //    var identity = new ClaimsIdentity(new Claim[]
        //    {
        //        new Claim(ClaimTypes.Role, user.Role),
        //        new Claim(ClaimTypes.Email,$"{user.Email}")
        //    });

        //    var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = identity,
        //        Expires = DateTime.Now.AddSeconds(10),
        //        SigningCredentials = credentials
        //    };
        //    var token = jwtTokenHandler.CreateToken(tokenDescriptor);
        //    return jwtTokenHandler.WriteToken(token);
        //}

        private string CreateJwt(User user)
        {
            if (user == null)
            {
                // Handle the case where the user is null
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(GenerateRandomKey(256));


            // Ensure that user.Role and user.Email are not null before using them
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, user.Role_Id.HasValue ? user.Role_Id.Value.ToString() : string.Empty),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty)
            };

            var identity = new ClaimsIdentity(claims);

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddSeconds(10),
                SigningCredentials = credentials
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }

        private static string GenerateRandomKey(int length)
        {
            byte[] keyBytes = new byte[length / 8];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(keyBytes);
            }

            return Convert.ToBase64String(keyBytes);
        }


    }
}
