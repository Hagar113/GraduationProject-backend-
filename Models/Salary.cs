namespace GraduationProject.Models
{
    public class Salary
    {
        public int Id { get; set; }
        public double NetSalary { get; set; }
        public double? ExtraSalary { get; set; }
        public double? SalaryLoss { get; set; }
        public double? TotalSalary { get; set; }
    }
}
