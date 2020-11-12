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
            if (!context.Raitings.Any())
            {
                for (var i = 2; i <= 5; i++)
                {
                    context.Raitings.Add(new Raiting {Name = i.ToString()});
                }

                context.SaveChanges();
            }

            if (!context.Foods.Any())
            {
                context.Foods.Add(new Food {Name = "3 раза в день"});
                context.Foods.Add(new Food {Name = "2 раза в день"});
                context.Foods.Add(new Food {Name = "Без питания"});
            }
            
            if (!context.Roles.Any())
            {
                var adminRole = new Role { RoleId = 1, Name = "admin" };
                var userRole = new Role { RoleId = 2, Name = "user" };

                context.Roles.Add(adminRole);
                context.Roles.Add(userRole);
                context.SaveChanges();
            }

            if (!context.Countries.Any())
            {
                using var fs = new FileStream("Data/cities.json", FileMode.Open);
                var array = new byte[fs.Length];
                fs.Read(array, 0, array.Length);
                var text = Encoding.Default.GetString(array);
                var jObj = JObject.Parse(text);

                foreach (var kvp in jObj.Cast<KeyValuePair<string, JToken>>().ToList())
                {
                    var country = new Country {Name = kvp.Key};
                    context.Countries.Add(country);

                    foreach (var value in kvp.Value)
                    {
                        var town = new Town {Name = value.ToString(), Country = country};
                        context.Towns.Add(town);
                    }
                }

                context.SaveChanges();
            }
        }
    }
}