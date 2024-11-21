﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SWE30003_Group5_Koala.Data;
using SWE30003_Group5_Koala.Models;
using System.Linq;

namespace SWE30003_Group5_Koala.Services
{
    public class DatabaseInitializer
    {
        public static void Initialize(KoalaDbContext context)
        {
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