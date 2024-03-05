namespace GraduationProject.DTOs
{
    public class GetEmpResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string phone { get; set; }
        public string Nationality { get; set; }
        public string NationalId { get; set; }
        public string Address { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime Contractdate { get; set; }
        public TimeSpan AttendanceTime { get; set; }
        public TimeSpan LeaveTime { get; set; }
        public int? GId { get; set; }
        public double? NetSalary { get; set; }

    }
}
