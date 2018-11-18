using System;
using System.Collections.Generic;
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

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
