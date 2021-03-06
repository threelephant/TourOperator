using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.IO;
using System.Net;

namespace TourOperator.Models
{
    public class TourOperatorContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Raiting> Raitings { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public TourOperatorContext(DbContextOptions<TourOperatorContext> options) : base(options) 
        {
            // Database.EnsureDeleted();
            // Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tour>()
                .HasOne(t => t.FlightArrival)
                .WithMany(a => a.ToursArrival)
                .HasForeignKey(t => t.FlightArrivalId);

            modelBuilder.Entity<Tour>()
                .HasOne(t => t.FlightDeparture)
                .WithMany(a => a.ToursDestination)
                .HasForeignKey(t => t.FlightDepartureId);

            modelBuilder.Entity<Flight>()
                .HasOne(f => f.AirportArrival)
                .WithMany(a => a.FlightsArrival)
                .HasForeignKey(f => f.AirportArrivalId);
            
            modelBuilder.Entity<Flight>()
                .HasOne(f => f.AirportDeparture)
                .WithMany(a => a.FlightsDeparture)
                .HasForeignKey(f => f.AirportDepartureId);
        }
    }
}