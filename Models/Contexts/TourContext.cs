using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net;

namespace TourOperator.Models
{
    public class TourOperatorContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Raiting> Raitings { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public TourOperatorContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.Entity<Tour>()
                .HasOne(t => t.AirportArrival)
                .WithMany(a => a.ToursArrival)
                .HasForeignKey(t => t.AirportArrivalId);

            modelBuilder.Entity<Tour>()
                .HasOne(t => t.AirportDeparture)
                .WithMany(a => a.ToursDestination)
                .HasForeignKey(t => t.AirportDepartureId);
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