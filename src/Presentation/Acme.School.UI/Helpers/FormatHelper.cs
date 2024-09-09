namespace Acme.School.UI.Helpers
{
    public static class FormatHelper
    {
        public static string FormatDate(DateTime? date)
        {
            if (date is null)
            {
                return string.Empty;
            }
            return date.Value.ToString("dd/MM/yyyy");

        }
        public static string FormatDateWithTime(DateTime? date)
        {
            if (date is null)
            {
                return string.Empty;
            }
            return date.Value.ToString("dd/MM/yyyy HH:mm");
        }
    }
}
