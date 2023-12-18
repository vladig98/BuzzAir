using BuzzAir.Models.DbModels;
using BuzzAir.Models.DbModels.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BuzzAir.Data
{
    public class BuzzAirDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
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
        public DbSet<ApplicationUser> AppUsers { get; set; }
        public DbSet<BookingFlight> BookingFlights { get; set; }
        public DbSet<BookingPassenger> BookingPassengers { get; set; }
        public DbSet<FlightPassenger> FlightPassengers { get; set; }
        public DbSet<PersonService> PersonServices { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<UserBooking> UserBookings { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<AirportCheckIn> AirportCheckIns { get; set; }
        public DbSet<Baggage> Baggages { get; set; }
        public DbSet<Flexibility> Flexibilities { get; set; }
        public DbSet<OnTimeArrival> OnTimeArrivals { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Timezone> Timezones { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<FlightSeat> FlightSeats { get; set; }

        public BuzzAirDbContext(DbContextOptions<BuzzAirDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var decimalProps = builder.Model.GetEntityTypes().SelectMany(t => t.GetProperties())
                    .Where(p => (System.Nullable.GetUnderlyingType(p.ClrType) ?? p.ClrType) == typeof(decimal));

            foreach (var property in decimalProps)
            {
                property.SetPrecision(18);
                property.SetScale(2);
            }

            builder.Entity<Flight>().HasOne(x => x.Origin).WithMany(x => x.FlightsFromOrigin).HasForeignKey(x => x.OriginId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Flight>().HasOne(x => x.Destination).WithMany(x => x.FlightsToDestination).HasForeignKey(x => x.DestinationId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<TravelDocument>().HasOne(x => x.Nationality).WithMany(x => x.Nationalities).HasForeignKey(x => x.NationalityId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<TravelDocument>().HasOne(x => x.BirthCountry).WithMany(x => x.BirthCountries).HasForeignKey(x => x.BirthCountryId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Address>().HasOne(x => x.City).WithMany(x => x.Addresses).HasForeignKey(x => x.CityId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Address>().HasOne(x => x.State).WithMany(x => x.Addresses).HasForeignKey(x => x.StateId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Airport>().HasOne(x => x.City).WithMany(x => x.Airports).HasForeignKey(x => x.CityId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Airport>().HasOne(x => x.State).WithMany(x => x.Airports).HasForeignKey(x => x.StateId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<City>().HasOne(x => x.State).WithMany(x => x.Cities).HasForeignKey(x => x.StateId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<City>().HasOne(x => x.Country).WithMany(x => x.Cities).HasForeignKey(x => x.CountryId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
