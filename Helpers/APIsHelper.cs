namespace GraduationProject.Helpers
{
    public static class APIsHelper
    {
        public static int? GetNumberOfWeekdaysInMonth(string weekDay)
        {
            DayOfWeek targetDayOfWeek;
            if (!Enum.TryParse(weekDay, true, out targetDayOfWeek))
            {
               return null;
            }

            int numberOfDays = 0;
            DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            for (DateTime date = firstDayOfMonth; date <= lastDayOfMonth; date = date.AddDays(1))
            {
                if (date.DayOfWeek == targetDayOfWeek)
                {
                    numberOfDays++;
                }
            }

            return numberOfDays;
        }

    }
}
