using BuzzAir.Data.Configurations;

namespace BuzzAir.Data.Data;

public class BuzzAirDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public DbSet<Aircraft> Aircrafts { get; set; }
    public DbSet<Airport> Airports { get; set; }
    public DbSet<ApplicationUser> AppUsers { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<BookingFlight> BookingFlights { get; set; }
    public DbSet<BookingPassenger> BookingPassengers { get; set; }
    public DbSet<ChangeLog> ChangeLogs { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Flight> Flights { get; set; }
    public DbSet<FlightPassenger> FlightPassengers { get; set; }
    public DbSet<Passenger> Passengers { get; set; }
    public DbSet<PassengerService> PassengerServices { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<State> States { get; set; }
    public DbSet<Timezone> Timezones { get; set; }
    public DbSet<TravelDocument> TravelDocuments { get; set; }
    public DbSet<AirportCheckIn> AirportCheckIns { get; set; }
    public DbSet<Baggage> Baggages { get; set; }
    public DbSet<Flexibility> Flexibilities { get; set; }
    public DbSet<OnTimeArrival> OnTimeArrivals { get; set; }
    public DbSet<Priority> Priorities { get; set; }
    public DbSet<Seat> Seats { get; set; }

    private readonly IHttpContextAccessor? _httpContextAccessor;

    public BuzzAirDbContext(DbContextOptions<BuzzAirDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
        Database.SetCommandTimeout(180);
    }

    public BuzzAirDbContext() { }

    public override int SaveChanges()
    {
        OverrideDateUpdated();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OverrideDateUpdated();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        OverrideDateUpdated();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        OverrideDateUpdated();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void OverrideDateUpdated()
    {
        DateTime now = DateTime.UtcNow;
        string userId = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
            throw new InvalidOperationException("No user in context");

        IEnumerable<EntityEntry> entities = ChangeTracker.Entries()
            .Where(e => e.State is EntityState.Modified or EntityState.Added or EntityState.Deleted);

        foreach (EntityEntry entity in entities)
        {
            IEntityType meta = entity.Metadata;
            HashSet<string> navNames =
            [
                .. meta.GetNavigations().Select(n => n.Name),
                .. meta.GetSkipNavigations().Select(sn => sn.Name),
            ];

            Dictionary<string, string?> before = meta.GetProperties().Where(p => !navNames.Contains(p.Name)).ToDictionary(p => p.Name, p => entity.OriginalValues[p]?.ToString());
            Dictionary<string, string?> after = meta.GetProperties().Where(p => !navNames.Contains(p.Name)).ToDictionary(p => p.Name, p => entity.CurrentValues[p]?.ToString());

            ChangeLog changeLog = new()
            {
                EntityName = meta.ClrType.Name,
                EntityId = string.Join(",", meta.FindPrimaryKey()?.Properties.Select(p => entity.Property(p.Name).CurrentValue?.ToString()) ?? []) ?? string.Empty,
                UserId = userId,
                TimestampUTC = now
            };

            if (entity.State == EntityState.Added)
            {
                changeLog.BeforeJSON = null;
                changeLog.AfterJSON = after == null || after.Count == 0 ? null : JsonConvert.SerializeObject(after);
                changeLog.Action = ChangeType.Added;
            }

            if (entity.State == EntityState.Deleted)
            {
                changeLog.BeforeJSON = before == null || before.Count == 0 ? null : JsonConvert.SerializeObject(before);
                changeLog.AfterJSON = null;
                changeLog.Action = ChangeType.Deleted;
            }

            if (entity.State == EntityState.Modified)
            {
                changeLog.BeforeJSON = before == null || before.Count == 0 ? null : JsonConvert.SerializeObject(before);
                changeLog.AfterJSON = after == null || after.Count == 0 ? null : JsonConvert.SerializeObject(after);
                changeLog.Action = ChangeType.Modified;
            }

            _ = ChangeLogs.Add(changeLog);
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        if (builder is null)
        {
            return;
        }

        _ = builder.ApplyConfiguration(new AircraftConfiguration());
        _ = builder.ApplyConfiguration(new AirportConfiguration());
        _ = builder.ApplyConfiguration(new ApplicationUserConfiguration());
        _ = builder.ApplyConfiguration(new BookingConfiguration());
        _ = builder.ApplyConfiguration(new BookingFlightConfiguration());
        _ = builder.ApplyConfiguration(new BookingPassengerConfiguration());
        _ = builder.ApplyConfiguration(new ChangeLogConfiguration());
        _ = builder.ApplyConfiguration(new CityConfiguration());
        _ = builder.ApplyConfiguration(new CountryConfiguration());
        _ = builder.ApplyConfiguration(new FlightConfiguration());
        _ = builder.ApplyConfiguration(new FlightPassengerConfiguration());
        _ = builder.ApplyConfiguration(new PassengerConfiguration());
        _ = builder.ApplyConfiguration(new PassengerServiceConfiguration());
        _ = builder.ApplyConfiguration(new PaymentConfiguration());
        _ = builder.ApplyConfiguration(new ServiceConfiguration());
        _ = builder.ApplyConfiguration(new StateConfiguration());
        _ = builder.ApplyConfiguration(new TimezoneConfiguration());
        _ = builder.ApplyConfiguration(new TravelDocumentConfiguration());
    }
}
