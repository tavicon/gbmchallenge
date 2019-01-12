using System;

namespace GBM.Challenge.Tools.Config.Contracts
{
    public interface IConfigProvider
    {
        string GetString(string key);
        bool GetBoolean(string key);
        int GetInt(string key);
        float GetFloat(string key);
        double GetDouble(string key);
        decimal GetDecimal(string key);
        DateTime GetDateTime(string key);
        DateTime GetDateTime(string key, IFormatProvider provider);
    }
}
