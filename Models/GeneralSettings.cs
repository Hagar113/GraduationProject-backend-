using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models
{
    public class GeneralSettings
    {
        //[Key]
        //public int id { get; set; }
        //public int? Deduction { get; set; }
        //public int? Addition { get; set; }
        //public string? selectedFirstWeekendDay { get; set; }
        //public string? selectedSecondWeekendDay { get; set; }
    
    
    
    
        [Key]
        public int Id { get; set; }

        public int? Deduction { get; set; }  

        public int? Addition { get; set; }  

        public string SelectedFirstWeekendDay { get; set; } 

        public string SelectedSecondWeekendDay { get; set; } 
    }
}
