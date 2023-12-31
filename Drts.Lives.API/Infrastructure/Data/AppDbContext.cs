﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Live> Lives { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(GetStringConection());
                base.OnConfiguring(optionsBuilder);
            }
        }

        public static string GetStringConection()
        {
            return "Host=localhost; Database=DrtsLive; Username=postgres; Password=vssql";
        }
    }
}
