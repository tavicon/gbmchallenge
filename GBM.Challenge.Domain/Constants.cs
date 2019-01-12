using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBM.Challenge.Domain
{
    public struct Constants
    {
        public const string DATABASE = "SqlConnectionString";
        public const string STORAGE = "AzureStorageConnectionString";
        public const string WATNAME = "AzureStorageTableName";
        public const string MAXATTEMPTS = "RetryPolicyAzureTableMaxAttempts";
        public const string WAITSECONDS = "RetryPolicyAzureTableWaitSeconds";
    }
}
