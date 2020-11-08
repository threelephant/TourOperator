using System.Collections.Generic;

namespace TourOperator.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
    }    
}