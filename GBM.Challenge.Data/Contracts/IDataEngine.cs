using GBM.Challenge.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBM.Challenge.Data.Contracts
{
    public interface IDataEngine : IDisposable
    {
        Task SetCurrentPosition(IGeographicalPosition currentPosition);
        Task<IGeographicalPosition> GetCurrentPosition(Guid taxyId, Guid travelId);
        Task SetPositionHistory(IGeographicalPosition position);
        Task<ICollection<IGeographicalPosition>> GetPositionsByTravelHistory(Guid taxyId, Guid travelId);
    }
}
