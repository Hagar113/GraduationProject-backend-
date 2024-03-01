using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Models
{
    public class Holiday
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public virtual List<HolidayDay>? HolidayDays { get; set; }
        [ForeignKey("company")]
        public int? C_Id { get; set; }
        public virtual Company? Company { get; set; }
    }
}
