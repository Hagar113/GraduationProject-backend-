namespace GraduationProject.DTOs
{
    public class GeneralSettingDTO
    {
        
        public int? Deduction { get; set; }

        public int? Addition { get; set; }
        public string? Method { get; set; }
        public string SelectedFirstWeekendDay { get; set; }

        public string SelectedSecondWeekendDay { get; set; }
        public List<int>? SelectedVacationDays { get; set; }

    }
}
