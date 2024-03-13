namespace GraduationProject.Models
{
    public class Page
    {
        public int id { get; set; }
        public string name { get; set; }
        public string? routerLink { get; set; }
        public string? icon { get; set; }
        public string? label { get; set; }
        public string? activeRoute { get; set; }
        public virtual List<RolePermission>? RolePermissions { get; set; }
    }
}
