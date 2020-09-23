using System;

namespace TourOperator.Models
{
    public class Tour
    {
        public long UserId { get; set; }
        public User User { get; set; }
        public long HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public DateTime TimeArrival { get; set; }
        public DateTime TimeDeparture { get; set; }
    }
}