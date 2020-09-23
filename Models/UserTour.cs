using System;

namespace TourOperator.Models
{
    public class UserTour
    {
        public long UserTourId { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public long TourId { get; set; }
        public Tour Tour { get; set; }
        public DateTime? TimeBooking { get; set; }
        public DateTime? TimePurchase { get; set; }
    }
}