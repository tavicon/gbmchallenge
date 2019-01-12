using GBM.Challenge.Tools.Config.Contracts;
using System;
using System.Globalization;

namespace GBM.Challenge.Tools.Config
{
    public abstract class ConfigProviderBase : IConfigProvider
    {
        public string GetString(string key)
        {
            return GetParameter(key);
        }

        public string GetStringWithDefault(string key, string defaultValue)
        {
            string val = GetParameter(key);

            return val ?? defaultValue;
        }

        public bool GetBoolean(string key)
        {
            string val = GetParameter(key).Trim();

            if (val.Equals("1"))
            {
                return true;
            }

            if (val.Equals("0"))
            {
                return false;
            }

            return Convert.ToBoolean(val);
        }

        public bool GetBooleanWithDefault(string key, bool defaultValue)
        {
            try
            {
                string val = GetParameter(key);

                if (string.IsNullOrEmpty(val))
                {
                    return defaultValue;
                }

                val = val.Trim();

                if (val.Equals("1"))
                {
                    return true;
                }

                if (val.Equals("0"))
                {
                    return false;
                }

                bool valResult;

                if (bool.TryParse(val, out valResult))
                {
                    return valResult;
                }

                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        public DateTime GetDateTime(string key)
        {
            string val = GetParameter(key);

            return DateTime.Parse(val);
        }

        public DateTime GetDateTime(string key, IFormatProvider provider)
        {
            string val = GetParameter(key);

            return DateTime.Parse(val, provider);
        }

        public DateTime GetDateTimeWithDefault(string key, DateTime defaultValue)
        {
            try
            {
                string val = GetParameter(key);

                if (string.IsNullOrEmpty(val))
                {
                    return defaultValue;
                }

                DateTime valResult;

                if (DateTime.TryParse(val, out valResult))
                {
                    return valResult;
                }

                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        public DateTime GetDateTimeWithDefault(string key, DateTime defaultValue, IFormatProvider provider)
        {
            try
            {
                string val = GetParameter(key);

                if (string.IsNullOrEmpty(val))
                {
                    return defaultValue;
                }

                DateTime valResult;

                if (DateTime.TryParse(val, provider, DateTimeStyles.None, out valResult))
                {
                    return valResult;
                }

                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        public decimal GetDecimal(string key)
        {
            string val = GetParameter(key);

            return Convert.ToDecimal(val);
        }

        public decimal GetDecimalWithDefault(string key, decimal defaultValue)
        {
            try
            {
                string val = GetParameter(key);

                if (string.IsNullOrEmpty(val))
                {
                    return defaultValue;
                }

                decimal valResult;

                if (decimal.TryParse(val, out valResult))
                {
                    return valResult;
                }

                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        public float GetFloat(string key)
        {
            string val = GetParameter(key);

            return float.Parse(val);
        }

        public float GetFloatWithDefault(string key, float defaultValue)
        {
            try
            {
                string val = GetParameter(key);

                if (string.IsNullOrEmpty(val))
                {
                    return defaultValue;
                }

                float valResult;

                if (float.TryParse(val, out valResult))
                {
                    return valResult;
                }

                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        public double GetDouble(string key)
        {
            string val = GetParameter(key);

            return Convert.ToDouble(val);
        }

        public double GetDoubleWithDefault(string key, double defaultValue)
        {
            try
            {
                string val = GetParameter(key);

                if (string.IsNullOrEmpty(val))
                {
                    return defaultValue;
                }

                double valResult;

                if (double.TryParse(val, out valResult))
                {
                    return valResult;
                }

                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        public int GetInt(string key)
        {
            string val = GetParameter(key);

            return Convert.ToInt32(val);
        }

        public int GetIntWithDefault(string key, int defaultValue)
        {
            try
            {
                string val = GetParameter(key);

                if (string.IsNullOrEmpty(val))
                {
                    return defaultValue;
                }

                int valResult;

                if (int.TryParse(val, out valResult))
                {
                    return valResult;
                }

                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        public long GetLong(string key)
        {
            string val = GetParameter(key);

            return Convert.ToInt64(val);
        }

        public long GetLongWithDefault(string key, long defaultValue)
        {
            try
            {
                string val = GetParameter(key);

                if (string.IsNullOrEmpty(val))
                {
                    return defaultValue;
                }

                long valResult;

                if (long.TryParse(val, out valResult))
                {
                    return valResult;
                }

                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        public short GetShort(string key)
        {
            string val = GetParameter(key);

            return Convert.ToInt16(val);
        }

        public short GetShortWithDefault(string key, short defaultValue)
        {
            try
            {
                string val = GetParameter(key);

                if (string.IsNullOrEmpty(val))
                {
                    return defaultValue;
                }

                short valResult;

                if (short.TryParse(val, out valResult))
                {
                    return valResult;
                }

                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        public TimeSpan GetTimeSpan(string key)
        {
            string val = GetParameter(key);

            return TimeSpan.Parse(val);
        }

        public TimeSpan? GetTimeSpanNullable(string key)
        {
            try
            {
                string val = GetParameter(key);

                return TimeSpan.Parse(val);
            }
            catch
            {
                return null;
            }
        }

        public TimeSpan? GetTimeSpanNullableWithDefault(string key, TimeSpan? defaultValue)
        {
            try
            {
                string val = GetParameter(key);

                if (string.IsNullOrEmpty(val))
                {
                    return defaultValue;
                }

                TimeSpan valResult;

                if (TimeSpan.TryParse(val, out valResult))
                {
                    return valResult;
                }

                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        public TimeSpan GetTimeSpanWithDefault(string key, TimeSpan defaultValue)
        {
            try
            {
                string val = GetParameter(key);

                if (string.IsNullOrEmpty(val))
                {
                    return defaultValue;
                }

                TimeSpan valResult;

                if (TimeSpan.TryParse(val, out valResult))
                {
                    return valResult;
                }

                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        internal abstract string GetParameter(string keyName);
    }
}
