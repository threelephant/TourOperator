using System.Collections.Generic;

namespace TourOperator.Models
{
    public class Airport
    {
        public int AirportId { get; set; }
        public string Name { get; set; }
        public int TownId { get; set; }
        public Town Town { get; set; }
        public List<Tour> ToursArrival { get; set; }
        public List<Tour> ToursDestination { get; set; }
    }
}