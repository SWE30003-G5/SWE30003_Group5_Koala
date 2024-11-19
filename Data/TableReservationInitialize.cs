using SWE30003_Group5_Koala.Models;
using SWE30003_Group5_Koala.Data;
using System.Diagnostics;

namespace SWE30003_Group5_Koala.Data
{
    public static class DbInitializer
    {
        public static void Initialize(KoalaDbContext context)
        {
            // Look for any students.
            if (context.Tables.Any())
            {
                return;   // DB has been seeded
            }

            var tables = new Table[]
           {
                new Table{Id = 13, IsAvailable = true},
                new Table{Id = 12, IsAvailable = false},
           };

            context.Tables.AddRange(tables);
            context.SaveChanges();

        }
    }
}