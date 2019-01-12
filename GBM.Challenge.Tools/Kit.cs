using GBM.Challenge.Tools.TimeZone;
using System;

namespace GBM.Challenge.Tools
{
    public static class Kit
    {
        public static DateTime GetDateTime()
        {
            return ConvertTimeZone.ConvertFromUtcToTimeZoneMexico(DateTime.UtcNow);
        }

        public static int GetYear()
        {
            return ConvertTimeZone.ConvertFromUtcToTimeZoneMexico(DateTime.UtcNow).Year;
        }

        public static int GetMonth()
        {
            return ConvertTimeZone.ConvertFromUtcToTimeZoneMexico(DateTime.UtcNow).Month;
        }

        public static string GetDateTimeStringFormatISO(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-ddTHH:mm:ss");
        }
    }
}
