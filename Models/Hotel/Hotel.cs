using System.Collections.Generic;

namespace TourOperator.Models
{
    public class Hotel
    {
        public long HotelId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PricePerNight { get; set; }
        public string Image { get; set; }
        public int RaitingId { get; set; }
        public Raiting Raiting { get; set; }
        public int FoodId { get; set; }
        public Food Food { get; set; }
        public int TownId { get; set; }
        public Town Town { get; set; }
        public List<Tour> Tours { get; set; }
        public List<Review> Reviews { get; set; }
        public Hotel()
        {
            Reviews = new List<Review>();
        }
    }
}