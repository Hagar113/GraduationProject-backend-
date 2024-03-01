using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Models
{
    public class HolidayDay
    {
        public int id { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("holiday")]
        public int H_Id { get; set; }
        public  virtual Holiday holiday { get; set;}
    }
}

