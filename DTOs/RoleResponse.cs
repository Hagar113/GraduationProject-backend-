namespace GraduationProject.DTOs
{
    public class RoleResponse
    {
        public int? role_Id { get; set; }
        public string? role_Name { get; set; }
        public List<RolePermissionsResponse>? rolePermissionsDTOs { get; set; }

    }
    public class RolePermissionsResponse
    {
        public int? RolePermission_Id { get; set; }
        public bool? isAdd { get; set; }
        public bool? isEdit { get; set; }
        public bool? isDelete { get; set; }
        public bool? isView { get; set; }
        public int? page_Id { get; set; }
        public string? page_Name { get; set; }
        public string? icon { get; set; }
        public string? label { get; set; }
        public string? routerLink { get; set; }
        public string? activateRoute { get; set; }
    }
}
