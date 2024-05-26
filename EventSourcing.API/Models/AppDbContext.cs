﻿using Microsoft.EntityFrameworkCore;

namespace EventSourcing.API.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
                
        }

        public DbSet<Category> Category { get; set; }
    }
}
