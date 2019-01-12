using GBM.Challenge.Domain;
using GBM.Challenge.Domain.Models;
using GBM.Challenge.Tools.Config;
using GBM.Challenge.Tools.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBM.Challenge.Data
{
    public class DataEngine : IDisposable
    {
        protected bool IsDisposed;

        public async Task SetCurrentPosition(GeographicalPosition currentPosition)
        {
            using (DataManager dataManager = new DataManager(SettingProvider.Instance.GetString(Constants.DATABASE_MASTER), Provider.SqlServer))
            {
                IDataParameter[] parameters = new IDataParameter[6];

                parameters[0] = new SqlParameter("@Id", currentPosition.Id);
                parameters[1] = new SqlParameter("@TaxyId", currentPosition.TaxyId);
                parameters[2] = new SqlParameter("@TravelId", currentPosition.TravelId);
                parameters[3] = new SqlParameter("@Latitude", currentPosition.Latitude);
                parameters[4] = new SqlParameter("@Longitude", currentPosition.Longitude);
                parameters[5] = new SqlParameter("@RegistrationDate", currentPosition.RegistrationDate);

                dataManager.Execute("[gbm].[SetCurrentPosition]", parameters);
            }
        }

        public async Task GetCurrentPosition(Guid taxyId , Guid travelId)
        {
            using (DataManager dataManager = new DataManager(SettingProvider.Instance.GetString(Constants.DATABASE_MASTER), Provider.SqlServer))
            {
                IDataParameter[] parameters = new IDataParameter[2];

                parameters[0] = new SqlParameter("@TaxyId", taxyId);
                parameters[1] = new SqlParameter("@TravelId", travelId);

                dataManager.Execute("[gbm].[GetCurrentPosition]", parameters);
            }
        }

        public async Task SetPosition(GeographicalPosition currentPosition)
        {
            using (DataManager dataManager = new DataManager(SettingProvider.Instance.GetString(Constants.DATABASE_MASTER), Provider.SqlServer))
            {
                IDataParameter[] parameters = new IDataParameter[6];

                parameters[0] = new SqlParameter("@Id", currentPosition.Id);
                parameters[1] = new SqlParameter("@TaxyId", currentPosition.TaxyId);
                parameters[2] = new SqlParameter("@TravelId", currentPosition.TravelId);
                parameters[3] = new SqlParameter("@Latitude", currentPosition.Latitude);
                parameters[4] = new SqlParameter("@Longitude", currentPosition.Longitude);
                parameters[5] = new SqlParameter("@RegistrationDate", currentPosition.RegistrationDate);

                dataManager.Execute("[gbm].[SetPosition]", parameters);
            }
        }

        public async Task<ICollection<GeographicalPosition>> GetPositionsByTravel(Guid taxyId, Guid travelId)
        {
            using (DataManager dataManager = new DataManager(SettingProvider.Instance.GetString(Constants.DATABASE_MASTER), Provider.SqlServer))
            {
                IDataParameter[] parameters = new IDataParameter[2];

                parameters[0] = new SqlParameter("@TaxyId", taxyId);
                parameters[1] = new SqlParameter("@TravelId", travelId);

                using (IDataReader reader = dataManager.ExecuteReader("[gbm].[GetPositionsByTravel]", parameters))
                {
                    var positions = new List<GeographicalPosition>();

                    while (reader.Read())
                    {
                        var position = new GeographicalPosition();

                        position.Id = reader["Id"] == DBNull.Value ? Guid.Empty : Guid.Parse(reader["Id"].ToString());
                        position.Latitude = reader["Latitude"] == DBNull.Value ? 0 : decimal.Parse(reader["Latitude"].ToString());
                        position.Longitude = reader["Longitude"] == DBNull.Value ? 0 : decimal.Parse(reader["Longitude"].ToString());
                        position.RegistrationDate = reader["RegistrationDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["RegistrationDate"]);
                        positions.Add(position);
                    }

                    return positions;
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
