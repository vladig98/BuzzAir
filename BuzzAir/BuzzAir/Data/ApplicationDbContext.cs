using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BuzzAir.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BuzzAir.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Aircraft> Aircrafts { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<TravelDocument> TravelDocuments { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<BookingFlight> BookingFlights { get; set; }
        public DbSet<BookingPassenger> BookingPassengers { get; set; }
        public DbSet<FlightPassenger> FlightPassengers { get; set; }
        public DbSet<PersonService> PersonServices { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<UserBooking> UserBookings { get; set; }
        public DbSet<Person> People { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var property in builder.Model.GetEntityTypes()
                                                 .SelectMany(t => t.GetProperties())
                                                 .Where(p => p.ClrType == typeof(decimal)))
            {
                property.Relational().ColumnType = "decimal(18, 6)";
            }
        }
    }
}
