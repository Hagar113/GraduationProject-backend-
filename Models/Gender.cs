using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models
{
    public class Gender
    {
        [Key]
        public int Id { get; set; }
        public string GName { get; set; }
    }
}