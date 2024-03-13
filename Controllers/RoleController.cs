using GraduationProject.DTOs;
using GraduationProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security;

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
                List<Role> roles = _context.Roles.ToList();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("GetRoleById{id}")]
        public ActionResult GetRoleById(int id)
        {
            try
            {
                var role = _context.Roles.Include(z => z.RolesPermissions).ThenInclude(z => z.page).FirstOrDefault(z => z.Id == id);
                SaveRoleDTO saveRoleDTO = new SaveRoleDTO();
                saveRoleDTO.role_Id = role.Id;
                saveRoleDTO.role_Name = role.Name;
                saveRoleDTO.rolePermissionsDTOs = [];
                foreach (var permision in role.RolesPermissions)
                {
                    saveRoleDTO.rolePermissionsDTOs.Add(new RolePermissionsDTO
                    {
                        RolePermission_Id = permision.id,
                        isAdd = permision.IsAdd,
                        isDelete = permision.IsDelete,
                        isEdit = permision.IsEdit,
                        isView = permision.IsView,
                        page_Id = permision.Page_Id,
                        page_Name = permision.page.name,
                    });

                }
                return Ok(saveRoleDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetListOfPages")]
        public ActionResult GetListOfPages()
        {
            try
            {
                var pages = _context.Pages.ToList();
                SaveRoleDTO saveRoleDTO = new SaveRoleDTO();
                saveRoleDTO.role_Id = 0;
                saveRoleDTO.role_Name = "";
                saveRoleDTO.rolePermissionsDTOs = [];

                foreach (var page in pages)
                {
                    saveRoleDTO.rolePermissionsDTOs.Add(new RolePermissionsDTO
                    {
                        RolePermission_Id = 0,
                        isAdd = false,
                        isDelete = false,
                        isEdit = false,
                        isView = false,
                        page_Id = page.id,
                        page_Name = page.name,
                    });
                }
                return Ok(saveRoleDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("saveRole")]
        public ActionResult SaveRole([FromBody] SaveRoleDTO saveRoleDTO)
        {
            try
            {
                if (saveRoleDTO.role_Id == 0)
                {
                    //For Add 
                    return add(saveRoleDTO);
                }
                else
                {
                    // edit
                    var role = _context.Roles.Include(h => h.RolesPermissions).FirstOrDefault(z => z.Id == saveRoleDTO.role_Id);
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
                            Page_Id = access.page_Id,
                            IsAdd = access.isAdd.Value,
                            IsDelete = access.isDelete.Value,
                            IsEdit = access.isEdit.Value,
                            IsView = access.isView.Value,
                        });
                    }
                    role.RolesPermissions = accessRules;

                    _context.Entry(role).State = EntityState.Modified;
                    _context.SaveChanges();
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("DeleteRole")]
        public ActionResult DeleteRole([FromBody] int id)
        {
            try
            {
                var role = _context.Roles.Include(z => z.RolesPermissions).FirstOrDefault(h => h.Id == id);
                _context.Roles.Remove(role);
                _context.SaveChanges();
                return Ok(new { message = "تم الحذف بنجاح" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // this function for add a new role with list of permissions 
        [HttpPost("add")]
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
                    Page_Id = access.page_Id,
                    IsAdd = access.isAdd.Value,
                    IsDelete = access.isDelete.Value,
                    IsEdit = access.isEdit.Value,
                    IsView = access.isView.Value,
                });
            }
            role.RolesPermissions = accessRules;

            _context.Roles.Add(role);
            _context.SaveChanges();
            return Ok();
        }


    }
}
