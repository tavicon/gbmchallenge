using GBM.Challenge.Domain.Contracts;
using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace GBM.Challenge.Data.Entities
{
    public class GeographicalPositionEntity : TableEntity, IGeographicalPosition
    {
        public Guid Id { get; set; }
        public Guid TaxyId { get; set; }
        public Guid DriverId { get; set; }
        public Guid TravelId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime RegistrationDate { get; set; }

        public GeographicalPositionEntity() { }
        public GeographicalPositionEntity(IGeographicalPosition position)
        {
            this.Id = position.Id;
            this.TaxyId = position.TaxyId;
            this.DriverId = position.DriverId;
            this.TravelId = position.TravelId;
            this.Latitude = position.Latitude;
            this.Longitude = position.Longitude;
            this.RegistrationDate = position.RegistrationDate;
            this.PartitionKey = position.TravelId.ToString().ToUpperInvariant();
            this.RowKey = position.TaxyId.ToString().ToUpperInvariant();
        }
    }
}
