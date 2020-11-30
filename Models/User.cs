using System.Collections.Generic;

namespace TourOperator.Models
{
    public class User
    {
        public long UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
        public List<Review> Reviews { get; set; }
        public List<Tour> Tours { get; set; }
    }
}