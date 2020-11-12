using System.Collections.Generic;

namespace TourOperator.Models
{
    public class Flight
    {
        public int FlightId { get; set; }
        public int AirportArrivalId { get; set; }
        public Airport AirportArrival { get; set; }
        public int AirportDepartureId { get; set; }
        public Airport AirportDeparture { get; set; }
        public decimal Price { get; set; }
        public List<Tour> ToursArrival { get; set; }
        public List<Tour> ToursDestination { get; set; }
    }
}