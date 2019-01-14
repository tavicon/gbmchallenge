using GBM.Challenge.Tools.Exception;
using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace GBM.Challenge.Tools.Data.Sql
{
    public class DataManager : IDisposable
    {
        protected bool IsDisposed = false;

        private const string FAIL_ARGUMENT = "Falta especificar el parámetro o su valor es inválido";

        protected DbConnection connection;

        private readonly string connectionString;

        private readonly Provider providerKind;

        private TimeSpan commandTimeout;

        public DataManager(string connectionString, Provider provider)
            : this(connectionString, provider, TimeSpan.FromMinutes(5))
        {
        }

        public DataManager(string connectionString, Provider provider, TimeSpan commandTimeout)
        {
            if (string.IsNullOrEmpty(connectionString.Trim()))
            {
                throw new ArgumentException(FAIL_ARGUMENT, "string connectionString");
            }

            this.connectionString = connectionString;
            this.providerKind = provider;
            this.commandTimeout = commandTimeout;
        }

        public DbConnection GetConnection()
        {
            if (this.connection == null)
            {
                CreateConnection();
            }
            return this.connection;
        }

        private void CreateConnection()
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory(GetProvider());
            this.connection = factory.CreateConnection();
            this.connection.ConnectionString = this.connectionString;
        }

        private string GetProvider()
        {
            string providerName = "System.Data.SqlClient";

            switch (this.providerKind)
            {
                case Provider.Oracle:
                    providerName = "System.Data.OracleClient";
                    break;
            }

            return providerName;
        }

        public void OpenConnection()
        {
            RetryPolicy retryPolicyConnection = RetryManager.Instance.GetDefaultSqlConnectionRetryPolicy();

            (retryPolicyConnection ?? RetryPolicy.NoRetry).ExecuteAction(() =>
            {
                if (this.connection == null)
                {
                    CreateConnection();
                }

                this.connection.Open();
            });
        }

        public void CloseConnection()
        {
            if (this.connection != null && this.connection.State == ConnectionState.Open)
            {
                this.connection.Close();
                this.connection = null;
            }
        }

        protected IDbCommand MakeCommand(string commandText, CommandType commandKind, IDataParameter[] parameters)
        {
            IDbCommand command = connection.CreateCommand();
            command.CommandText = commandText;
            command.CommandType = commandKind;

            if (parameters != null)
            {
                foreach (IDataParameter parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }

            return command;
        }

        public object Execute(string commandText, IDataParameter[] parameters)
        {
            if (string.IsNullOrEmpty(commandText.Trim()))
            {
                throw new ArgumentException(FAIL_ARGUMENT, "string commandText");
            }

            using (SqlConnection sqlCnnctn = new SqlConnection(this.connectionString))
            {
                object returnValue = null;
                try
                {
                    using (SqlCommand sqlCmmnd = sqlCnnctn.CreateCommand())
                    {
                        sqlCmmnd.CommandType = CommandType.StoredProcedure;
                        sqlCmmnd.CommandText = commandText;
                        sqlCmmnd.CommandTimeout = Convert.ToInt32(commandTimeout.TotalSeconds);
                        if (parameters != null)
                        {
                            foreach (IDataParameter parameter in parameters)
                            {
                                sqlCmmnd.Parameters.Add(parameter);
                            }
                        }

                        RetryPolicy retryPolicyConnection = RetryManager.Instance.GetDefaultSqlConnectionRetryPolicy();
                        (retryPolicyConnection ?? RetryPolicy.NoRetry).ExecuteAction(() =>
                        {
                            sqlCnnctn.Open();
                        });

                        RetryPolicy retryPolicyCommand = RetryManager.Instance.GetDefaultSqlCommandRetryPolicy();
                        (retryPolicyCommand ?? RetryPolicy.NoRetry).ExecuteAction(() =>
                        {
                            returnValue = sqlCmmnd.ExecuteScalar();
                        });

                        return returnValue;
                    }
                }
                catch (System.Exception sqlEx)
                {
                    throw new PlatformException(sqlEx.Message, sqlEx);
                }
                finally
                {
                    if (sqlCnnctn.State == ConnectionState.Open)
                    {
                        sqlCnnctn.Close();
                        sqlCnnctn.Dispose();
                    }
                }
            }
        }


        public IDataReader ExecuteReader(string commandText, IDataParameter[] parameters)
        {
            if (string.IsNullOrEmpty(commandText.Trim()))
            {
                throw new ArgumentException(FAIL_ARGUMENT, "string commandText");
            }

            SqlConnection sqlCnnctn = new SqlConnection(this.connectionString);
            SqlDataReader reader = null;
            try
            {
                using (SqlCommand sqlCmmnd = sqlCnnctn.CreateCommand())
                {
                    sqlCmmnd.CommandType = CommandType.StoredProcedure;
                    sqlCmmnd.CommandText = commandText;
                    sqlCmmnd.CommandTimeout = Convert.ToInt32(commandTimeout.TotalSeconds);
                    if (parameters != null)
                    {
                        foreach (IDataParameter parameter in parameters)
                        {
                            sqlCmmnd.Parameters.Add(parameter);
                        }
                    }

                    RetryPolicy retryPolicyConnection = RetryManager.Instance.GetDefaultSqlConnectionRetryPolicy();
                    (retryPolicyConnection ?? RetryPolicy.NoRetry).ExecuteAction(() =>
                    {
                        sqlCnnctn.Open();
                    });

                    RetryPolicy retryPolicyCommand = RetryManager.Instance.GetDefaultSqlCommandRetryPolicy();
                    (retryPolicyCommand ?? RetryPolicy.NoRetry).ExecuteAction(() =>
                    {
                        reader = sqlCmmnd.ExecuteReader(CommandBehavior.CloseConnection);
                    });

                    return reader;
                }
            }
            catch (System.Exception sqlEx)
            {
                if (sqlCnnctn.State == ConnectionState.Open)
                {
                    sqlCnnctn.Close();
                    sqlCnnctn.Dispose();
                }

                throw new PlatformException(sqlEx.Message, sqlEx);
            }
        }

        public void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            this.CloseConnection();

            GC.SuppressFinalize(this);
            IsDisposed = true;
        }
    }
}
