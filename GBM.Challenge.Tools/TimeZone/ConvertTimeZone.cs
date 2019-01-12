using System;

namespace GBM.Challenge.Tools.TimeZone
{
    public static class ConvertTimeZone
    {
        public static DateTime ConvertToUtc(DateTime dateTime)
        {
            return TimeZoneInfo.ConvertTimeToUtc(dateTime);
        }

        public static DateTime ConvertFromUtcToOtherTimeZone(DateTime dateTimeUtc, TimeZoneInfo timeZoneInfo)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(dateTimeUtc, timeZoneInfo);
        }

        public static DateTime ConvertFromUtcToTimeZoneMexico(DateTime dateTimeUtc)
        {
            return ConvertFromUtcToOtherTimeZone(dateTimeUtc, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)"));
        }
    }
}
