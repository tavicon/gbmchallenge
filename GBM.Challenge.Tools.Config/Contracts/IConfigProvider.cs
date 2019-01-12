using System;

namespace GBM.Challenge.Tools.Config.Contracts
{
    public interface IConfigProvider
    {
        string GetString(string key);
        string GetStringWithDefault(string key, string defaultValue);
        bool GetBoolean(string key);
        bool GetBooleanWithDefault(string key, bool defaultValue);
        int GetInt(string key);
        int GetIntWithDefault(string key, int defaultValue);
        short GetShort(string key);
        short GetShortWithDefault(string key, short defaultValue);
        float GetFloat(string key);
        float GetFloatWithDefault(string key, float defaultValue);
        long GetLong(string key);
        long GetLongWithDefault(string key, long defaultValue);
        double GetDouble(string key);
        double GetDoubleWithDefault(string key, double defaultValue);
        decimal GetDecimal(string key);
        decimal GetDecimalWithDefault(string key, decimal defaultValue);
        DateTime GetDateTime(string key);
        DateTime GetDateTime(string key, IFormatProvider provider);
        DateTime GetDateTimeWithDefault(string key, DateTime defaultValue);
        DateTime GetDateTimeWithDefault(string key, DateTime defaultValue, IFormatProvider provider);
        TimeSpan GetTimeSpan(string key);
        TimeSpan GetTimeSpanWithDefault(string key, TimeSpan defaultValue);
        TimeSpan? GetTimeSpanNullable(string key);
        TimeSpan? GetTimeSpanNullableWithDefault(string key, TimeSpan? defaultValue);
    }
}
