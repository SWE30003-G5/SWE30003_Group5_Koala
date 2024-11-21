using SWE30003_Group5_Koala.Data;
using SWE30003_Group5_Koala.Models;

namespace SWE30003_Group5_Koala.Services
{
    public class DatabaseInitializer
    {
        public static void Initialize(KoalaDbContext context)
        {
            // Seed admin user data
            if (!context.Users.Any(u => u.Role == "Admin"))
            {
                var adminUser = new User
                {
                    Name = "John Wick",
                    Role = "Admin",
                    Email = "johnwick@gmail.com",
                    Password = "Admin@1234",
                    PhoneNumber = "1234567890"
                };
                context.Users.Add(adminUser);
                context.SaveChanges();
            }
            // Seed menu items data
            if (!context.MenuItems.Any())
            {
                var menuItems = new[]
                {
                    new MenuItem { ID = 1, Name = "Crab Cake", Price = 12, ImageLocation = "Crab_cake.jpg", IsAvailable = false },
                    new MenuItem { ID = 2, Name = "Risotto", Price = 12, ImageLocation = "Risotto.jpg", IsAvailable = true },
                    new MenuItem { ID = 3, Name = "Beef Wellington", Price = 25, ImageLocation = "Beef_wellington.jpg", IsAvailable = true },
                    new MenuItem { ID = 4, Name = "Baked Salmon", Price = 18, ImageLocation = "Baked_salmon.jpg", IsAvailable = true },
                    new MenuItem { ID = 5, Name = "Fried Chicken", Price = 15, ImageLocation = "Fried_chicken.jpg", IsAvailable = true },
                    new MenuItem { ID = 6, Name = "Vegetable Soup", Price = 10, ImageLocation = "Vegetable_soup.jpg", IsAvailable = true },
                    new MenuItem { ID = 7, Name = "Ice Cream", Price = 3, ImageLocation = "Ice_cream.jpg", IsAvailable = true },
                    new MenuItem { ID = 8, Name = "Coca Cola", Price = 2, ImageLocation = "Coca_cola.jpg", IsAvailable = true }
                };
                context.MenuItems.AddRange(menuItems);
                context.SaveChanges();
            }
            // Seed tables data
            if (!context.Tables.Any())
            {
                var table = new Table
                {
                    Id = 1,
                    IsAvailable = true
                };
                context.Tables.Add(table);
                context.SaveChanges();
            }
        }
    }
}
