using System.Collections.Generic;
using System.Security;

namespace TourOperator.Models
{
    public class Hotel
    {
        public long HotelId { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public List<Tour> Tours { get; set; }
        public Hotel()
        {
            Tours = new List<Tour>();
        }
    }
}