using System.Collections.Generic;

namespace TourOperator.Models
{
    public class Flight
    {
        public int FlightId { get; set; }
        public int AirportId { get; set; }
        public Airport Airport { get; set; }
        public decimal Price { get; set; }
        public List<Tour> ToursArrival { get; set; }
        public List<Tour> ToursDestination { get; set; }
    }
}