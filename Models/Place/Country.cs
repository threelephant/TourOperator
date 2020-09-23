using System.Collections.Generic;

namespace TourOperator.Models
{
    public class Country
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
        public List<Town> Towns { get; set; }
    }
}