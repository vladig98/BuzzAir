﻿using BuzzAir.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BuzzAir.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext()
        {

        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Aircraft> Aircrafts { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<TravelDocument> TravelDocuments { get; set; }
        public DbSet<ApplicationUser> AppUsers { get; set; }
        public DbSet<BookingFlight> BookingFlights { get; set; }
        public DbSet<BookingPassenger> BookingPassengers { get; set; }
        public DbSet<FlightPassenger> FlightPassengers { get; set; }
        public DbSet<PersonService> PersonServices { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<UserBooking> UserBookings { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<AirportFlight> AirportFlights { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            foreach (var property in builder.Model.GetEntityTypes()
                                                 .SelectMany(t => t.GetProperties())
                                                 .Where(p => p.ClrType == typeof(decimal)))
            {
                property.Relational().ColumnType = "decimal(18, 6)";
            }

            builder
            .Entity<AirportFlight>()
            .HasOne<Airport>(e => e.Airport)
            .WithMany(e => e.Flights)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
