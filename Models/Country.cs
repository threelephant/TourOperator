using System.Collections.Generic;
using System.Security;

namespace TourOperator.Models
{
    public class Country
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
        public List<Hotel> Hotels { get; set; }
    }
}