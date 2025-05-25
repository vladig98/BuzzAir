using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BuzzAir.Data
{
    public class BuzzAirDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Aircraft> Aircrafts { get; set; }
        public virtual DbSet<Airport> Airports { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<Passenger> Passengers { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Seat> Seats { get; set; }
        public virtual DbSet<TravelDocument> TravelDocuments { get; set; }
        public virtual DbSet<ApplicationUser> AppUsers { get; set; }
        public virtual DbSet<BookingFlight> BookingFlights { get; set; }
        public virtual DbSet<BookingPassenger> BookingPassengers { get; set; }
        public virtual DbSet<FlightPassenger> FlightPassengers { get; set; }
        public virtual DbSet<PersonService> PersonServices { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<UserBooking> UserBookings { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<AirportCheckIn> AirportCheckIns { get; set; }
        public virtual DbSet<Baggage> Baggages { get; set; }
        public virtual DbSet<Flexibility> Flexibilities { get; set; }
        public virtual DbSet<OnTimeArrival> OnTimeArrivals { get; set; }
        public virtual DbSet<Priority> Priorities { get; set; }
        public virtual DbSet<Timezone> Timezones { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<FlightSeat> FlightSeats { get; set; }

        public BuzzAirDbContext(DbContextOptions<BuzzAirDbContext> options) : base(options)
        {
        }

        public BuzzAirDbContext()
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
