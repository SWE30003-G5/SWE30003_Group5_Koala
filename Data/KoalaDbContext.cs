using Microsoft.EntityFrameworkCore;
using SWE30003_Group5_Koala.Models;

namespace SWE30003_Group5_Koala.Data
{
    public class KoalaDbContext : DbContext
    {
        public KoalaDbContext(DbContextOptions<KoalaDbContext> option) : base(option) { }

        // Place models here when we have them.
        public DbSet<User> Users { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Table> Tables { get; set; }
    }
}
