using TourOperator.Models;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace TourOperator.Data
{
    public static class DbInitializer
    {
        public static void Initialize(TourOperatorContext context)
        {
            if (!context.Roles.Any())
            {
                Role adminRole = new Role { RoleId = 1, Name = "admin" };
                Role userRole = new Role { RoleId = 2, Name = "user" };

                context.Roles.Add(adminRole);
                context.Roles.Add(userRole);
                context.SaveChanges();
            }
            
            if (context.Countries.Any())
            {
                return;
            }

            using (FileStream fs = new FileStream("Data/cities.json", FileMode.Open))
            {
                var array = new byte[fs.Length];
                fs.Read(array, 0, array.Length);
                var text = Encoding.Default.GetString(array);
                var jObj = JObject.Parse(text);

                foreach (var kvp in jObj.Cast<KeyValuePair<string, JToken>>().ToList())
                {
                    Country country = new Country { Name = kvp.Key };
                    context.Countries.Add(country);

                    foreach (var value in kvp.Value)
                    {
                        Town town = new Town { Name = value.ToString(), Country = country };
                        context.Towns.Add(town);
                    }
                }
                context.SaveChanges();
            }
        }
    }
}