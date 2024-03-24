using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Models
{
    public class EmployeeAttendance
    {
        public int Id { get; set; }
        public DateTime Attendence  { get; set; }
        public DateTime? Departure { get; set;}

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public virtual Employee? Employee { get; set; }

    }
}
