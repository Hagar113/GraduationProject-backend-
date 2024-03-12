namespace GraduationProject.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public int GenderId { get; set; }

        public string Nationality { get; set; }
        public DateOnly BirthDate { get; set; }
        public string NationalId { get; set; }
        public DateOnly Contractdate { get; set; }
        public double NetSalary { get; set; }
        public string AttendanceTime { get; set; }
        public string LeaveTime { get; set; }
    }
}
