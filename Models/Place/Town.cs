using System.Collections.Generic;

namespace TourOperator.Models
{
    public class Town
    {
        public int TownId { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public List<Hotel> Hotels { get; set; }
        public List<Airport> Airports { get; set; }
    }
}