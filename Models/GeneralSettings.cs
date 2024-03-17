using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models
{
    public class GeneralSettings
    {
        
    
    
    
        [Key]
        public int Id { get; set; }

        public int? Deduction { get; set; }

        public int? Addition { get; set; }
        
        public string? Method { get; set; }

        public string SelectedFirstWeekendDay { get; set; } 

        public string SelectedSecondWeekendDay { get; set; } 
    }
}
