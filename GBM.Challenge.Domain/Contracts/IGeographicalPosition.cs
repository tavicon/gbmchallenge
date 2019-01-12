using System;

namespace GBM.Challenge.Domain.Contracts
{
    public interface IGeographicalPosition
    {
        Guid Id { get; set; }
        Guid TaxyId { get; set; }
        Guid DriverId { get; set; }
        Guid TravelId { get; set; }
        string Latitude { get; set; }
        string Longitude { get; set; }
        DateTime RegistrationDate { get; set; }
    }
}
