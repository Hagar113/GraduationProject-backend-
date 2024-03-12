namespace GraduationProject.DTOs
{
    public class SaveRoleDTO
    {
        public int? role_Id { get; set; }
        public string? role_Name { get; set; }
        public List<RolePermissionsDTO>? rolePermissionsDTOs { get; set; }
    }
    public class RolePermissionsDTO
    {
        public int? RolePermission_Id { get; set; }
        public bool? isAdd { get; set; }
        public bool? isEdit { get; set; }
        public bool? isDelete { get; set; }
        public bool? isView { get; set; }
        public string? page_Name { get; set; }
        public int? page_Id { get; set; }
    }
}
