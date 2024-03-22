namespace GraduationProject.DTOs
{
    public class EmployeesAttendancesResponse
    {
        public int id { get; set; }
        public string? empName { get; set; }
        public string? deptName { get; set; }
        public string? day { get; set; }
        public string? attendance { get; set; }
        public string? departure { get; set; }
    }
}
