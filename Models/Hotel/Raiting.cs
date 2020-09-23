using System.Collections.Generic;

namespace TourOperator.Models
{
    public class Raiting
    {
        public int RaitingId { get; set; }
        public string Name { get; set; }
        public List<Hotel> Hotels { get; set; }
    }
}