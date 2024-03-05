namespace GraduationProject.Models
{
    public class Page
    {
        public int id { get; set; }
        public string name { get; set; }
        public virtual List<RolePermission>? RolePermissions { get; set; }
    }
}
