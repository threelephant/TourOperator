using System.Collections.Generic;

namespace TourOperator.Models
{
    public class Food
    {
        public int FoodId { get; set; }
        public string Name { get; set; }
        public List<Hotel> Hotels { get; set; }
    }
}