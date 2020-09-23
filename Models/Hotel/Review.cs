using System;

namespace TourOperator.Models
{
    public class Review
    {
        public long ReviewId { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public long HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public decimal UserRaiting { get; set; }
        public string Text { get; set; }
    }
}