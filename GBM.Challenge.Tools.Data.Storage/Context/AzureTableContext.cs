using GBM.Challenge.Tools.Exception;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace GBM.Challenge.Tools.Data.Storage.Context
{
    internal sealed class AzureTableContext : IDisposable
    {
        private bool IsDisposed = false;
        private CloudTableClient _clienteTableAzure;

        internal AzureTableContext(string uriTableAzure, StorageCredentials credentials, int maxAttempts, int waitSeconds)
        {
            _clienteTableAzure = new CloudTableClient(new Uri(uriTableAzure), credentials);
            _clienteTableAzure.DefaultRequestOptions.RetryPolicy = new ExponentialRetry(TimeSpan.FromSeconds(waitSeconds), maxAttempts);
        }

        internal void CreateTableIfNotExists(string tableName)
        {
            _clienteTableAzure.GetTableReference(tableName).CreateIfNotExists();
        }

        internal void InsertOrReplaceEntity(ITableEntity entity, string tableName)
        {
            try
            {
                var referenciaTablaAzure = _clienteTableAzure.GetTableReference(tableName);
                referenciaTablaAzure.Execute(TableOperation.InsertOrReplace(entity));
            }
            catch (System.Exception stgEx)
            {
                throw new PlatformException(stgEx.Message, stgEx);
            }
        }

        internal T GetEntity<T>(string tableName, string partitionKey, string rowKey)
            where T : ITableEntity, new()
        {
            T result = default(T);

            try
            {
                var azureTable = _clienteTableAzure.GetTableReference(tableName);

                var queryResult = azureTable.Execute(TableOperation.Retrieve<T>(partitionKey, rowKey));

                result = (T)Convert.ChangeType(queryResult.Result, typeof(T));
            }
            catch (System.Exception stgEx)
            {
                throw new PlatformException(stgEx.Message, stgEx);
            }
            
            return result;
        }

        public void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            IsDisposed = true;
            GC.SuppressFinalize(this);
        }
    }
}
