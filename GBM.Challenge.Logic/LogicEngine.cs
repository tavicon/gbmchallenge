using GBM.Challenge.Data;
using GBM.Challenge.Data.Contracts;
using GBM.Challenge.Domain;
using GBM.Challenge.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GBM.Challenge.Logic
{
    public class LogicEngine : IDisposable
    {
        private bool IsDisposed;
        private IDataEngine DataEngine;

        public LogicEngine(DataEngineKind dataEngineKind)
        {
            switch (dataEngineKind)
            {
                case DataEngineKind.Sql:
                    DataEngine = new SqlDataEngine();
                    break;
                case DataEngineKind.Storage:
                    DataEngine = new StorageDataEngine();
                    break;
                default:
                    DataEngine = new SqlDataEngine();
                    break;
            }
        }

        public async Task SetCurrentPosition(IGeographicalPosition currentPosition)
        {
            await DataEngine.SetCurrentPosition(currentPosition);
        }

        public async Task<IGeographicalPosition> GetCurrentPosition(Guid taxyId, Guid travelId)
        {
            return await DataEngine.GetCurrentPosition(taxyId, travelId);
        }

        public async Task SetPositionHistory(IGeographicalPosition position)
        {
            await DataEngine.SetPositionHistory(position);
        }

        public async Task<ICollection<IGeographicalPosition>> GetPositionsByTravelHistory(Guid taxyId, Guid travelId)
        {
            return await DataEngine.GetPositionsByTravelHistory(taxyId, travelId);
        }

        public void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            DataEngine.Dispose();
            GC.SuppressFinalize(this);

            this.IsDisposed = true;
        }
    }
}
