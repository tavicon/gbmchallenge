using GBM.Challenge.Data.Contracts;
using GBM.Challenge.Data.Entities;
using GBM.Challenge.Domain;
using GBM.Challenge.Domain.Contracts;
using GBM.Challenge.Domain.Models;
using GBM.Challenge.Tools.Config;
using GBM.Challenge.Tools.Data.Storage;
using GBM.Challenge.Tools.Exception;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GBM.Challenge.Data
{
    public class StorageDataEngine : IDataEngine
    {
        private bool IsDisposed;

        public async Task SetCurrentPosition(IGeographicalPosition currentPosition)
        {
            GeographicalPositionEntity currentPositionEntity = new GeographicalPositionEntity(currentPosition);

            using (AzureManager azureManager = new AzureManager(SettingProvider.Instance.GetString(Constants.STORAGE),
                SettingProvider.Instance.GetInt(Constants.MAXATTEMPTS), SettingProvider.Instance.GetInt(Constants.WAITSECONDS)))
            {
                azureManager.InsertOrReplaceEntity(SettingProvider.Instance.GetString(Constants.WATNAME), currentPositionEntity, true);

                await Task.CompletedTask;
            }
        }

        public async Task<IGeographicalPosition> GetCurrentPosition(Guid taxyId, Guid travelId)
        {
            using (AzureManager azureManager = new AzureManager(SettingProvider.Instance.GetString(Constants.STORAGE),
                SettingProvider.Instance.GetInt(Constants.MAXATTEMPTS), SettingProvider.Instance.GetInt(Constants.WAITSECONDS)))
            {
                string partitionKey = travelId.ToString().ToUpperInvariant();
                string rowKey = taxyId.ToString().ToUpperInvariant();

                var currentPositionWat = azureManager.GetEntity<GeographicalPositionEntity>(SettingProvider.Instance.GetString(Constants.WATNAME), partitionKey, rowKey);

                if (currentPositionWat != null)
                {
                    var currentPosition = new GeographicalPosition(currentPositionWat);

                    return await Task.FromResult(currentPosition);
                }
                else
                {
                    throw new LogicException("No se encontró la posición actual.");
                }
            }
        }

        public async Task SetPositionHistory(IGeographicalPosition position)
        {
            throw new NotImplementedException("Método no implementado.");
        }

        public async Task<ICollection<IGeographicalPosition>> GetPositionsByTravelHistory(Guid taxyId, Guid travelId)
        {
            throw new NotImplementedException("Método no implementado.");
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
