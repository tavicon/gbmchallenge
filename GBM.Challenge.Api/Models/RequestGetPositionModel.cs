using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBM.Challenge.Api.Models
{
    public class RequestGetPositionModel
    {
        public Guid TaxyId { get; set; }
        public Guid TravelId { get; set; }
        public Guid DriverId { get; set; }
    }
}