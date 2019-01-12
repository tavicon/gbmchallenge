using GBM.Challenge.Tools.Config.Contracts;
using System;

namespace GBM.Challenge.Tools.Config
{
    public abstract class ConfigProviderBase : IConfigProvider
    {
        public string GetString(string key)
        {
            return GetParameter(key);
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
        
        public decimal GetDecimal(string key)
        {
            string val = GetParameter(key);

            return Convert.ToDecimal(val);
        }
        
        public float GetFloat(string key)
        {
            string val = GetParameter(key);

            return float.Parse(val);
        }

        public double GetDouble(string key)
        {
            string val = GetParameter(key);

            return Convert.ToDouble(val);
        }
        
        public int GetInt(string key)
        {
            string val = GetParameter(key);

            return Convert.ToInt32(val);
        }

        internal abstract string GetParameter(string keyName);
    }
}
