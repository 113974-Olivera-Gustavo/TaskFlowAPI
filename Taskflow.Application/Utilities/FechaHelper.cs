namespace Taskflow.Application.Utilities
{
    public abstract class FechaHelper
    {
        public static DateTime? ToCustomFormatDateTime(string dateString)
        {
            if (string.IsNullOrEmpty(dateString))
            {
                return null;
            }

            var date = Convert.ToDateTime(dateString);
            return date;
        }

        public static string ToCustomFormatTimeString(DateTime? date)
        {
            return date.HasValue ? date.Value.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture) : string.Empty;
        }

        public static string ToCustomFormatString(DateTime? date)
        {
            return date.HasValue ? date.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : string.Empty;
        }

        public static string ToIsoDateFormatString(DateTime? date)
        {
            return date.HasValue ? date.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) : string.Empty;
        }

        public static int CountRunningDays(DateTime fechaInicio, DateTime fechaFin)
        {
            var diferencia = fechaFin.Date - fechaInicio.Date;
            return Math.Abs(diferencia.Days) + 1;
        }
    }
}
