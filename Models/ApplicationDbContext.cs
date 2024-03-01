using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions o) : base(o)
        {

        }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }
        public virtual DbSet<Holiday> Holidays { get; set; }
        public virtual DbSet<HolidayDay> HolidayDays { get; set; }
        public virtual DbSet<EmployeeAttendance> EmployeeAttendances { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Page> Pages { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Salary> Salaries { get; set; }
    }

}