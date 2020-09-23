using System.Collections.Generic;
using System.Security;

namespace TourOperator.Models
{
    public class User
    {
        public long UserId { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public bool isAdmin { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public List<Tour> Tours { get; set; }
        public User()
        {
            Tours = new List<Tour>();
        }
    }
}