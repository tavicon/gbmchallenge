using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;
using System;
using System.Collections.Generic;

namespace GBM.Challenge.Tools.Data.Config
{
    public static class SqlTopazBootstrapper
    {
        private const string ConnectionStrategyName = "gbm_strategy_connection";
        private const string CommandStrategyName = "gbm_strategy_command";

        private static bool gbInitialized;
        private static object goLock = new object();

        public static void LoadConfigSqlRetry()
        {
            if (gbInitialized)
            {
                return;
            }
            lock (goLock)
            {
                if (gbInitialized)
                {
                    return;
                }

                int connRetries = 3;
                TimeSpan connMinBackoff = TimeSpan.FromSeconds(5);
                TimeSpan connMaxBackoff = TimeSpan.FromSeconds(10);
                TimeSpan connDeltaBackoff = TimeSpan.FromSeconds(2);

                int commRetries = 3;
                TimeSpan commMinBackoff = TimeSpan.FromSeconds(5);
                TimeSpan commMaxBackoff = TimeSpan.FromSeconds(10);
                TimeSpan commDeltaBackoff = TimeSpan.FromSeconds(2);

                RetryStrategy retryStrategyConnection = new ExponentialBackoff(
                    ConnectionStrategyName,
                    connRetries,
                    connMinBackoff,
                    connMaxBackoff,
                    connDeltaBackoff);

                RetryStrategy retryStrategyCommand = new ExponentialBackoff(
                    CommandStrategyName,
                    commRetries,
                    commMinBackoff,
                    commMaxBackoff,
                    commDeltaBackoff);

                var dictApplications = new Dictionary<string, string>
                {
                    { RetryManagerSqlExtensions.DefaultStrategyConnectionTechnologyName, retryStrategyConnection.Name },
                    { RetryManagerSqlExtensions.DefaultStrategyCommandTechnologyName, retryStrategyCommand.Name }
                };

                var retryManager = new RetryManager(
                    new[] { retryStrategyConnection, retryStrategyCommand },
                    retryStrategyCommand.Name,
                    dictApplications);

                RetryManager.SetDefault(retryManager, false);

                gbInitialized = true;
            }
        }
    }
}
