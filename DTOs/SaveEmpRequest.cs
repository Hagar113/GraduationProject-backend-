namespace GraduationProject.DTOs
{
    public class SaveEmpRequest
    {
        public int? id { get; set; }
        public int? EmpId { get; set; }
        public DateTime attendance { get; set; }
        public DateTime departure { get; set; }
    }
}
