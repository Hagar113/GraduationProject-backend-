using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Models
{
    public class TemporaryPasswords
    {
        public int Id { get; set; }

        [ForeignKey("user ")]
        public int? UserID{ get; set; }
        public virtual User? user { get; set; }
        public string? OriginalPassword { get; set; }

    }
}
