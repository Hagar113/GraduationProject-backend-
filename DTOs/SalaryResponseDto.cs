namespace GraduationProject.DTOs
{
    public class SalaryResponseDto
    {

        public string? empName { get; set; }
        public int id { get; set; }
        public string? deptName { get; set; }
        public double NetSalary { get; set; }
        public int attendanceDays { get; set; }
        public int absenceDays { get; set; }
        public double exrtaHours { get; set; }
        public double discountHours { get; set; }
        public double extraSalary { get; set; }
        public double discountSalary { get; set; }
        public double totalSalary { get; set; }
        //-------------
        public double HourlyRate { get; set; } // Add this line
        public double DailyRate { get; set; } // Add this line
        public int WeekendDays { get; set; } // Add this line

        public int Month { get; set; }
        public int Year { get; set; }
        public int AbsenceDaysToDate { get; set; }
    }
}
