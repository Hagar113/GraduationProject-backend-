using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Models
{
    public class Gender
    {
        [Key]
        public int Id { get; set; }
        public string GName { get; set; }
    }
}