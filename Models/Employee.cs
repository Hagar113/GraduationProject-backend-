using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string phone { get; set; }
        public string Nationality { get; set; }
        public string NationalId{ get; set; }
        public string Address { get; set; }
        public DateOnly Birthdate { get; set; }
        public DateOnly Contractdate { get; set; }
        public string AttendanceTime { get; set; }
        public string LeaveTime { get; set; }

        [ForeignKey("dept")]
        public int? deptid { get; set; }
        public virtual Department? dept { get; set; }
        [ForeignKey("gender")]
        public int? GId { get; set; }
        public virtual Gender? gender { get; set; }
        [ForeignKey("salary")]
        public int? Sal_ID { get; set; }
        public virtual Salary? salary { get; set; }

        [ForeignKey("user")]
        public int? user_Id { get; set; }
        public virtual User? user{ get; set; }
    }
}
