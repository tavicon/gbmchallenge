using System;

namespace GBM.Challenge.Domain.Models
{
    public class GeographicalPosition
    {
        public Guid Id { get; set; }
        public Guid TaxyId { get; set; }
        public Guid TravelId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
