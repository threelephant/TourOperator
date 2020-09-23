using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourOperator.Models
{
    public class Tour
    {
        public long TourId { get; set; }
        public long HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public byte AdultNumber { get; set; }
        public byte ChildNumber { get; set; }
        public byte InfantNumnber { get; set; }
        public decimal Price { get; set; }
        public int AirportArrivalId { get; set; }
        public Airport AirportArrival { get; set; }
        public int AirportDepartureId { get; set; }
        public Airport AirportDeparture { get; set; }
        public DateTime TimeArrival { get; set; }
        public DateTime TimeDeparture { get; set; }
        public List<UserTour> UserTours { get; set; }
        public Tour()
        {
            UserTours = new List<UserTour>();
        }
    }
}