using GraduationProject.DTOs;
using GraduationProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace GraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public RoleController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("GetAllRoles")]
        public ActionResult GetAllRoles()
        {
            try
            {
                List<Role> roles =_context.Roles.ToList();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        
        [HttpPost("saveRole")]
        public ActionResult SaveRole(SaveRoleDTO saveRoleDTO) 
        {
            try
            {
                if (saveRoleDTO.role_Id==0)
                {
                    //For Add 
                    return add(saveRoleDTO);
                }
                else
                {
                    // edit
                    var role = _context.Roles.Include(h => h.RolesPermissions).FirstOrDefault(z=>z.Id==saveRoleDTO.role_Id);
                    role.Name = saveRoleDTO.role_Name;
                    // this to delete the old list
                    foreach (var permission in role.RolesPermissions)
                    {
                        _context.Remove(permission);
                    }
                    //then add the new list
                    role.RolesPermissions = new List<RolePermission>();
                    List<RolePermission> accessRules = new List<RolePermission>();
                    foreach (var access in saveRoleDTO.rolePermissionsDTOs)
                    {
                        accessRules.Add(new RolePermission
                        {
                            IsAdd = access.isAdd,
                            IsDelete = access.isDelete,
                            IsIdit = access.isEdit,
                            IsView = access.isView,
                        });
                    }
                    role.RolesPermissions = accessRules;

                    _context.Entry(role).State=EntityState.Modified;
                    _context.SaveChanges();
                    return Ok();
                }
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteRole")]
        public ActionResult DeleteRole(int id)
        {
            try
            {
                var role = _context.Roles.FirstOrDefaultAsync(h => h.Id == id);
                _context.Remove(role);
                _context.SaveChanges();
                return Ok(new {message = "تم الحذف بنجاح"});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // this function for add a new role with list of permissions 
        public ActionResult add(SaveRoleDTO saveRole)
        {
            /// addd
            Role role = new Role();
            role.Name = saveRole.role_Name;
            role.RolesPermissions = new List<RolePermission>();

            List<RolePermission> accessRules = new List<RolePermission>();
            foreach (var access in saveRole.rolePermissionsDTOs)
            {
                accessRules.Add(new RolePermission
                {
                    IsAdd = access.isAdd,
                    IsDelete = access.isDelete,
                    IsIdit = access.isEdit,
                    IsView = access.isView,
                });
            }
            role.RolesPermissions = accessRules;

            _context.Roles.Add(role);
            _context.SaveChanges();
            return Ok();
        }


    }
}
