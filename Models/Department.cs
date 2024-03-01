using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Employee>? Employees {  get; set; }= new List<Employee>();
        [ForeignKey("companyy")]
        public int? Company_Id { get; set; }
        public virtual Company? companyy { get; set; }

    }
}
