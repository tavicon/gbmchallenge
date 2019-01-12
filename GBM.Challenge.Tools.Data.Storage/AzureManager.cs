using GBM.Challenge.Tools.Data.Storage.Context;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace GBM.Challenge.Tools.Data.Storage
{
    public class AzureManager : IDisposable
    {
        protected bool IsDisposed = false;

        private CloudStorageAccount cloudStorageAccount;
        private int maxAttempts;
        private int waitSeconds;

        public AzureManager(string connectionString, int maxAttempts, int waitSeconds)
        {
            this.cloudStorageAccount = CloudStorageAccount.Parse(connectionString);
            this.maxAttempts = maxAttempts;
            this.waitSeconds = waitSeconds;
        }

        public void InsertOrReplaceEntity<T>(string nombreTabla, T entidad, bool createTable = false) where T : ITableEntity
        {
            using (var context = new AzureTableContext(cloudStorageAccount.TableEndpoint.AbsoluteUri, cloudStorageAccount.Credentials, maxAttempts, waitSeconds))
            {
                if (createTable)
                {
                    context.CreateTableIfNotExists(nombreTabla);
                }

                context.InsertOrReplaceEntity(entidad, nombreTabla);
            }
        }

        public T GetEntity<T>(string nombreTabla, string partitionKey, string rowKey, bool createTable = false)
            where T : ITableEntity, new()
        {
            using (var context = new AzureTableContext(cloudStorageAccount.TableEndpoint.AbsoluteUri, cloudStorageAccount.Credentials, maxAttempts, waitSeconds))
            {
                if (createTable)
                {
                    context.CreateTableIfNotExists(nombreTabla);
                }

                return context.GetEntity<T>(nombreTabla, partitionKey, rowKey);
            }
        }

        public void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            this.cloudStorageAccount = null;

            GC.SuppressFinalize(this);
            IsDisposed = true;
        }
    }
}
