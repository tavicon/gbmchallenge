using GBM.Challenge.Tools.Config.Contracts;
using System;
using System.Configuration;

namespace GBM.Challenge.Tools.Config
{
    public class SettingProvider : ConfigProviderBase
    {
        public SettingProvider()
        {

        }

        private const string FAIL_ARGUMENT = "Falta especificar la clave del recurso";

        private static readonly object padlock = new object();

        private static IConfigProvider provider;

        public static IConfigProvider Instance
        {
            get
            {
                if (provider == null)
                {
                    lock (padlock)
                    {
                        if (provider == null)
                        {
                            provider = new SettingProvider();
                        }
                    }
                }

                return provider;
            }
        }

        internal override string GetParameter(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException(FAIL_ARGUMENT, "string key");
            }

            try
            {
                return ConfigurationManager.AppSettings[key];
            }
            catch
            {
                throw;
            }
        }
    }
}
