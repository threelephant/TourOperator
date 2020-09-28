using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourOperator.Models
{
    public class Tour
    {
        public long TourId { get; set; }
        public long HotelId { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public Hotel Hotel { get; set; }
        public byte AdultNumber { get; set; }
        public byte ChildNumber { get; set; }
        public byte InfantNumnber { get; set; }
        public decimal Price { get; set; }
        public int FlightArrivalId { get; set; }
        public Flight FlightArrival { get; set; }
        public int FlightDepartureId { get; set; }
        public Flight FlightDeparture { get; set; }
        public DateTime TimeArrival { get; set; }
        public DateTime TimeDeparture { get; set; }
        public DateTime TimeBooking { get; set; }
        public DateTime TimePurchase { get; set; }
    }
}