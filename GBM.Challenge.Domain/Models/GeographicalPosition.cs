﻿using GBM.Challenge.Domain.Contracts;
using System;

namespace GBM.Challenge.Domain.Models
{
    public class GeographicalPosition : IGeographicalPosition
    {
        public Guid Id { get; set; }
        public Guid TaxyId { get; set; }
        public Guid DriverId { get; set; }
        public Guid TravelId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime RegistrationDate { get; set; }

        public GeographicalPosition() { }
        public GeographicalPosition(IGeographicalPosition position)
        {
            this.Id = position.Id;
            this.TaxyId = position.TaxyId;
            this.DriverId = position.DriverId;
            this.TravelId = position.TravelId;
            this.Latitude = position.Latitude;
            this.Longitude = position.Longitude;
            this.RegistrationDate = position.RegistrationDate;
        }
    }
}
