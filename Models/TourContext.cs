using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Security;
using System.Net;

namespace TourOperator.Models
{
    public class TourOperatorContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Country> Countries { get; set; }
        public TourOperatorContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tour>()
                .HasKey(t => new { t.UserId, t.HotelId });

            modelBuilder.Entity<Tour>()
                .HasOne(t => t.User)
                .WithMany(u => u.Tours)
                .HasForeignKey(t => t.UserId);
            
            modelBuilder.Entity<Tour>()
                .HasOne(t => t.Hotel)
                .WithMany(h => h.Tours)
                .HasForeignKey(t => t.HotelId);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            IConfigurationRoot config = builder.Build();

            var password = new Security().GetPassword();
            string connectionString = config.GetConnectionString("DefaultConnection") + new NetworkCredential("", password).Password;

            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}