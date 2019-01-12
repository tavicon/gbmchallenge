using GBM.Challenge.Data.Contracts;
using GBM.Challenge.Domain;
using GBM.Challenge.Domain.Contracts;
using GBM.Challenge.Domain.Models;
using GBM.Challenge.Tools.Config;
using GBM.Challenge.Tools.Data.Sql;
using GBM.Challenge.Tools.Exception;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace GBM.Challenge.Data
{
    public class SqlDataEngine : IDataEngine
    {
        private bool IsDisposed;

        [Obsolete]
        public async Task SetCurrentPosition(IGeographicalPosition currentPosition)
        {
            using (DataManager dataManager = new DataManager(SettingProvider.Instance.GetString(Constants.DATABASE), Provider.SqlServer))
            {
                IDataParameter[] parameters = new IDataParameter[7];

                parameters[0] = new SqlParameter("@Id", currentPosition.Id);
                parameters[1] = new SqlParameter("@TaxyId", currentPosition.TaxyId);
                parameters[2] = new SqlParameter("@DriverId", currentPosition.DriverId);
                parameters[3] = new SqlParameter("@TravelId", currentPosition.TravelId);
                parameters[4] = new SqlParameter("@Latitude", currentPosition.Latitude);
                parameters[5] = new SqlParameter("@Longitude", currentPosition.Longitude);
                parameters[6] = new SqlParameter("@RegistrationDate", currentPosition.RegistrationDate);

                dataManager.Execute("[gbm].[SetCurrentPosition]", parameters);

                await Task.CompletedTask;
            }
        }

        [Obsolete]
        public async Task<IGeographicalPosition> GetCurrentPosition(Guid taxyId , Guid travelId)
        {
            using (DataManager dataManager = new DataManager(SettingProvider.Instance.GetString(Constants.DATABASE), Provider.SqlServer))
            {
                IDataParameter[] parameters = new IDataParameter[2];

                parameters[0] = new SqlParameter("@TaxyId", taxyId);
                parameters[1] = new SqlParameter("@TravelId", travelId);

                using (IDataReader reader = dataManager.ExecuteReader("[gbm].[GetCurrentPosition]", parameters))
                {
                    if (reader.Read())
                    {
                        var position = new GeographicalPosition
                        {
                            Id = reader["Id"] == DBNull.Value ? Guid.Empty : Guid.Parse(reader["Id"].ToString()),
                            TaxyId = reader["TaxyId"] == DBNull.Value ? Guid.Empty : Guid.Parse(reader["TaxyId"].ToString()),
                            TravelId = reader["TravelId"] == DBNull.Value ? Guid.Empty : Guid.Parse(reader["TravelId"].ToString()),
                            DriverId = reader["DriverId"] == DBNull.Value ? Guid.Empty : Guid.Parse(reader["DriverId"].ToString()),
                            Latitude = reader["Latitude"] == DBNull.Value ? "" : reader["Latitude"].ToString(),
                            Longitude = reader["Longitude"] == DBNull.Value ? "" : reader["Longitude"].ToString(),
                            RegistrationDate = reader["RegistrationDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["RegistrationDate"])
                        };

                        return await Task.FromResult(position);
                    }
                    else
                    {
                        throw new LogicException("No se encontró la posición actual.");
                    }
                }
            }
        }

        public async Task SetPositionHistory(IGeographicalPosition position)
        {
            using (DataManager dataManager = new DataManager(SettingProvider.Instance.GetString(Constants.DATABASE), Provider.SqlServer))
            {
                IDataParameter[] parameters = new IDataParameter[7];

                parameters[0] = new SqlParameter("@Id", position.Id);
                parameters[1] = new SqlParameter("@TaxyId", position.TaxyId);
                parameters[2] = new SqlParameter("@DriverId", position.DriverId);
                parameters[3] = new SqlParameter("@TravelId", position.TravelId);
                parameters[4] = new SqlParameter("@Latitude", position.Latitude);
                parameters[5] = new SqlParameter("@Longitude", position.Longitude);
                parameters[6] = new SqlParameter("@RegistrationDate", position.RegistrationDate);

                dataManager.Execute("[gbm].[SetPositionHistory]", parameters);

                await Task.CompletedTask;
            }
        }

        public async Task<ICollection<IGeographicalPosition>> GetPositionsByTravelHistory(Guid taxyId, Guid travelId)
        {
            using (DataManager dataManager = new DataManager(SettingProvider.Instance.GetString(Constants.DATABASE), Provider.SqlServer))
            {
                IDataParameter[] parameters = new IDataParameter[2];

                parameters[0] = new SqlParameter("@TaxyId", taxyId);
                parameters[1] = new SqlParameter("@TravelId", travelId);

                using (IDataReader reader = dataManager.ExecuteReader("[gbm].[GetPositionsByTravelHistory]", parameters))
                {
                    var positions = new List<IGeographicalPosition>();

                    while (reader.Read())
                    {
                        var position = new GeographicalPosition();

                        position.Id = reader["Id"] == DBNull.Value ? Guid.Empty : Guid.Parse(reader["Id"].ToString());
                        position.Latitude = reader["Latitude"] == DBNull.Value ? "" : reader["Latitude"].ToString();
                        position.Longitude = reader["Longitude"] == DBNull.Value ? "" : reader["Longitude"].ToString();
                        position.RegistrationDate = reader["RegistrationDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["RegistrationDate"]);
                        positions.Add(position);
                    }

                    return await Task.FromResult(positions);
                }
            }
        }

        public void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            GC.SuppressFinalize(this);

            this.IsDisposed = true;
        }
    }
}
