using Microsoft.EntityFrameworkCore;
using SWE30003_Group5_Koala.Models;

namespace SWE30003_Group5_Koala.Data
{
    public class KoalaDbContext : DbContext
    {
        public KoalaDbContext(DbContextOptions<KoalaDbContext> option) : base(option) { }

        // Place models here when we have them.
        public DbSet<MenuItem> MenuItems { get; set; }
    }
}
