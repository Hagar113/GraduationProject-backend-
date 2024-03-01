namespace GraduationProject.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<RolePermission>? RolesPermissions { get; set;}

    }
}
