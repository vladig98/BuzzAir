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

        List<IMutableEntityType> entityTypes = [.. builder.Model.GetEntityTypes()];

        IEnumerable<IMutableProperty> primaryKeys = entityTypes
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(string) && p.IsPrimaryKey());

        // ---- Global conventions ----
        // All string PKs get max length 450
        foreach (IMutableProperty primaryKey in primaryKeys)
        {
            _ = builder.Entity(primaryKey.DeclaringType.ClrType)
                       .Property(primaryKey.Name)
                       .HasMaxLength(450)
                       .IsRequired();
        }

        // ---- ApplicationUser ----
        _ = builder.Entity<ApplicationUser>(e =>
        {
            _ = e.HasOne(u => u.Passenger)
                 .WithOne(p => p.User)
                 .HasForeignKey<ApplicationUser>(u => u.PassengerId)
                 .OnDelete(DeleteBehavior.Restrict)
                 .IsRequired(false);
            _ = e.HasOne(u => u.City)
                 .WithMany(c => c.Users)
                 .HasForeignKey(u => u.CityId)
                 .OnDelete(DeleteBehavior.Restrict)
                 .IsRequired();
            _ = e.Property(u => u.FirstName).IsRequired().HasMaxLength(50);
            _ = e.Property(u => u.LastName).IsRequired().HasMaxLength(50);
            _ = e.Property(u => u.PostalCode).IsRequired().HasMaxLength(20);
            _ = e.Property(u => u.Street).IsRequired().HasMaxLength(150);
            _ = e.Property(u => u.Gender)
                 .IsRequired()
                 .HasConversion<string>();
            _ = e.Property(u => u.DateOfBirth)
                 .IsRequired();
        });

        // ---- BookingPassenger ----
        _ = builder.Entity<BookingPassenger>(e =>
        {
            _ = e.HasKey(bp => new { bp.PassengerId, bp.BookingId });
            _ = e.HasOne(bp => bp.Booking)
                 .WithMany(b => b.Passengers)
                 .HasForeignKey(bp => bp.BookingId)
                 .OnDelete(DeleteBehavior.Restrict)
                 .IsRequired();
            _ = e.HasOne(bp => bp.Passenger)
                 .WithMany(p => p.Bookings)
                 .HasForeignKey(bp => bp.PassengerId)
                 .OnDelete(DeleteBehavior.Restrict)
                 .IsRequired();
        });

        // ---- BookingFlight ----
        _ = builder.Entity<BookingFlight>(e =>
        {
            _ = e.HasKey(bf => new { bf.FlightId, bf.BookingId });
            _ = e.HasOne(bf => bf.Booking)
                 .WithMany(b => b.Flights)
                 .HasForeignKey(bf => bf.BookingId)
                 .OnDelete(DeleteBehavior.Restrict)
                 .IsRequired();
            _ = e.HasOne(bf => bf.Flight)
                 .WithMany(f => f.Bookings)
                 .HasForeignKey(bf => bf.FlightId)
                 .OnDelete(DeleteBehavior.Restrict)
                 .IsRequired();
        });

        // ---- Aircraft ----
        _ = builder.Entity<Aircraft>(e =>
        {
            _ = e.HasKey(a => a.Id);
            _ = e.Property(a => a.Name)
                 .IsRequired()
                 .HasMaxLength(100);
            _ = e.Property(a => a.NumberOfSeats).IsRequired();
            _ = e.Property(a => a.IsDeleted).IsRequired();
            _ = e.HasMany(a => a.Flights)
                 .WithOne(f => f.Aircraft)
                 .HasForeignKey(f => f.AircraftId)
                 .OnDelete(DeleteBehavior.Restrict);
        });

        // ---- Airport ----
        _ = builder.Entity<Airport>(e =>
        {
            _ = e.HasKey(a => a.Id);
            _ = e.Property(a => a.ICAO)
                 .IsRequired()
                 .HasMaxLength(4);
            _ = e.Property(a => a.IATA)
                 .IsRequired()
                 .HasMaxLength(3);
            _ = e.Property(a => a.Name)
                 .IsRequired()
                 .HasMaxLength(150);
            _ = e.Property(a => a.IsDeleted).IsRequired();
            _ = e.HasOne(a => a.City)
                 .WithMany(c => c.Airports)
                 .HasForeignKey(a => a.CityId)
                 .OnDelete(DeleteBehavior.Restrict)
                 .IsRequired();
            _ = e.HasMany(a => a.FlightsFrom)
                 .WithOne(f => f.Origin)
                 .HasForeignKey(f => f.OriginId)
                 .OnDelete(DeleteBehavior.Restrict);
            _ = e.HasMany(a => a.FlightsTo)
                 .WithOne(f => f.Destination)
                 .HasForeignKey(f => f.DestinationId)
                 .OnDelete(DeleteBehavior.Restrict);
            // optional coords
            _ = e.Property(a => a.Latitude).HasPrecision(9, 6);
            _ = e.Property(a => a.Longitude).HasPrecision(9, 6);
            _ = e.Property(a => a.ElevationAboveSeaLevel);
        });

        // ---- City ----
        _ = builder.Entity<City>(e =>
        {
            _ = e.HasKey(c => c.Id);
            _ = e.Property(c => c.Name)
                 .IsRequired()
                 .HasMaxLength(100);
            _ = e.Property(c => c.IsDeleted).IsRequired();
            _ = e.HasOne(c => c.Country)
                 .WithMany(cn => cn.Cities)
                 .HasForeignKey(c => c.CountryId)
                 .OnDelete(DeleteBehavior.Restrict)
                 .IsRequired();
            _ = e.HasOne(c => c.State)
                 .WithMany(s => s.Cities)
                 .HasForeignKey(c => c.StateId)
                 .OnDelete(DeleteBehavior.Restrict);
            _ = e.HasOne(c => c.Timezone)
                 .WithMany(tz => tz.Cities)
                 .HasForeignKey(c => c.TimezoneId)
                 .OnDelete(DeleteBehavior.Restrict)
                 .IsRequired();
            _ = e.HasMany(c => c.Airports)
                 .WithOne(a => a.City)
                 .HasForeignKey(a => a.CityId)
                 .OnDelete(DeleteBehavior.Restrict);
            _ = e.HasMany(c => c.Users)
                 .WithOne(u => u.City)
                 .HasForeignKey(u => u.CityId)
                 .OnDelete(DeleteBehavior.Restrict)
                 .IsRequired();
        });

        // ---- State ----
        _ = builder.Entity<State>(e =>
        {
            _ = e.HasKey(s => s.Id);
            _ = e.Property(s => s.Name)
                 .IsRequired()
                 .HasMaxLength(100);
            _ = e.Property(s => s.IsDeleted).IsRequired();
            _ = e.HasOne(s => s.Country)
                 .WithMany(c => c.States)
                 .HasForeignKey(s => s.CountryId)
                 .OnDelete(DeleteBehavior.Restrict)
                 .IsRequired();
            _ = e.HasMany(s => s.Cities)
                 .WithOne(c => c.State)
                 .HasForeignKey(c => c.StateId)
                 .OnDelete(DeleteBehavior.Restrict);
        });

        // ---- Country ----
        _ = builder.Entity<Country>(e =>
        {
            _ = e.HasKey(c => c.Id);
            _ = e.Property(c => c.Name)
                 .IsRequired()
                 .HasMaxLength(100);
            _ = e.Property(c => c.ISO)
                 .IsRequired()
                 .HasMaxLength(2);
            _ = e.Property(c => c.IsOfficiallyRecognizedCountry).IsRequired();
            _ = e.Property(c => c.IsDeleted).IsRequired();
            _ = e.HasMany(c => c.Cities)
                 .WithOne(ci => ci.Country)
                 .HasForeignKey(ci => ci.CountryId)
                 .OnDelete(DeleteBehavior.Restrict);
            _ = e.HasMany(c => c.States)
                 .WithOne(st => st.Country)
                 .HasForeignKey(st => st.CountryId)
                 .OnDelete(DeleteBehavior.Restrict);
            _ = e.HasMany(c => c.DocumentsNationalities)
                 .WithOne(td => td.Nationality)
                 .HasForeignKey(td => td.NationalityId)
                 .OnDelete(DeleteBehavior.Restrict);
            _ = e.HasMany(c => c.DocumentsBirthCountries)
                 .WithOne(td => td.BirthCountry)
                 .HasForeignKey(td => td.BirthCountryId)
                 .OnDelete(DeleteBehavior.Restrict);
        });

        // ---- Timezone ----
        _ = builder.Entity<Timezone>(e =>
        {
            _ = e.HasKey(tz => tz.Id);
            _ = e.Property(tz => tz.Name)
                 .IsRequired()
                 .HasMaxLength(100);
            _ = e.Property(tz => tz.Identifier)
                 .IsRequired()
                 .HasMaxLength(100);
            _ = e.Property(tz => tz.Abbreviation)
                 .IsRequired()
                 .HasMaxLength(10);
            _ = e.Property(tz => tz.UsesDST).IsRequired();
            _ = e.Property(tz => tz.IsDeleted).IsRequired();
            _ = e.Property(tz => tz.Offset).IsRequired();
            _ = e.HasMany(tz => tz.Cities)
                 .WithOne(c => c.Timezone)
                 .HasForeignKey(c => c.TimezoneId)
                 .OnDelete(DeleteBehavior.Restrict);
        });

        // ---- Flight ----
        _ = builder.Entity<Flight>(e =>
        {
            _ = e.HasKey(f => f.Id);
            _ = e.Property(f => f.FlightNumber)
                 .IsRequired()
                 .HasMaxLength(10);
            _ = e.Property(f => f.DepartureUTC).IsRequired();
            _ = e.Property(f => f.ArrivalUTC).IsRequired();
            _ = e.Property(f => f.PriceInEur)
                 .IsRequired()
                 .HasPrecision(18, 2);
            _ = e.Property(f => f.IsDeleted).IsRequired();
            _ = e.HasOne(f => f.Origin)
                 .WithMany(a => a.FlightsFrom)
                 .HasForeignKey(f => f.OriginId)
                 .OnDelete(DeleteBehavior.Restrict)
                 .IsRequired();
            _ = e.HasOne(f => f.Destination)
                 .WithMany(a => a.FlightsTo)
                 .HasForeignKey(f => f.DestinationId)
                 .OnDelete(DeleteBehavior.Restrict)
                 .IsRequired();
            _ = e.HasOne(f => f.Aircraft)
                 .WithMany(a => a.Flights)
                 .HasForeignKey(f => f.AircraftId)
                 .OnDelete(DeleteBehavior.Restrict)
                 .IsRequired();
            _ = e.HasMany(f => f.Passengers)
                 .WithOne(fp => fp.Flight)
                 .HasForeignKey(fp => fp.FlightId)
                 .OnDelete(DeleteBehavior.Restrict);
            _ = e.HasMany(f => f.Bookings)
                 .WithOne(bf => bf.Flight)
                 .HasForeignKey(bf => bf.FlightId)
                 .OnDelete(DeleteBehavior.Restrict);
            _ = e.Property(f => f.TakenSeats)
                 .IsRequired()
                 .HasMaxLength(2000);
        });

        // ---- FlightPassenger ----
        _ = builder.Entity<FlightPassenger>(e =>
        {
            _ = e.HasKey(fp => new { fp.FlightId, fp.PassengerId, fp.SeatNumber });
            _ = e.HasOne(fp => fp.Flight)
                 .WithMany(f => f.Passengers)
                 .HasForeignKey(fp => fp.FlightId)
                 .OnDelete(DeleteBehavior.Restrict)
                 .IsRequired();
            _ = e.HasOne(fp => fp.Passenger)
                 .WithMany(p => p.Flights)
                 .HasForeignKey(fp => fp.PassengerId)
                 .OnDelete(DeleteBehavior.Restrict)
                 .IsRequired();
            _ = e.Property(fp => fp.SeatNumber).IsRequired();
        });

        // ---- Passenger ----
        _ = builder.Entity<Passenger>(e =>
        {
            _ = e.HasKey(p => p.Id);
            _ = e.Property(p => p.FirstName)
                 .IsRequired()
                 .HasMaxLength(50);
            _ = e.Property(p => p.LastName)
                 .IsRequired()
                 .HasMaxLength(50);
            _ = e.Property(p => p.DateOfBirth).IsRequired();
            _ = e.Property(p => p.Gender).IsRequired();
            _ = e.HasOne(p => p.Document)
                 .WithOne(d => d.Passenger)
                 .HasForeignKey<TravelDocument>(d => d.PassengerId)
                 .OnDelete(DeleteBehavior.Restrict)
                 .IsRequired();
            _ = e.HasOne(p => p.User)
                 .WithOne(u => u.Passenger)
                 .HasForeignKey<ApplicationUser>(u => u.PassengerId)
                 .OnDelete(DeleteBehavior.Restrict);
            _ = e.HasMany(p => p.Services)
                 .WithOne(ps => ps.Passenger)
                 .HasForeignKey(ps => ps.PassengerId)
                 .OnDelete(DeleteBehavior.Restrict);
            _ = e.HasMany(p => p.Flights)
                 .WithOne(fp => fp.Passenger)
                 .HasForeignKey(fp => fp.PassengerId)
                 .OnDelete(DeleteBehavior.Restrict);
            _ = e.HasMany(p => p.Bookings)
                 .WithOne(bp => bp.Passenger)
                 .HasForeignKey(bp => bp.PassengerId)
                 .OnDelete(DeleteBehavior.Restrict);
        });

        // ---- Payment ----
        _ = builder.Entity<Payment>(e =>
        {
            _ = e.HasKey(p => p.Id);
            _ = e.Property(p => p.Card)
                 .IsRequired()
                 .HasConversion<string>();
            _ = e.Property(p => p.ExpiryDate).IsRequired();
            _ = e.Property(p => p.CardNumber)
                 .IsRequired()
                 .HasMaxLength(20);
            _ = e.Property(p => p.CardHolder)
                 .IsRequired()
                 .HasMaxLength(100);
            _ = e.Property(p => p.CVC)
                 .IsRequired()
                 .HasMaxLength(4);
            _ = e.Property(p => p.AmountInEur)
                 .IsRequired()
                 .HasPrecision(18, 2);
            _ = e.HasOne(p => p.Booking)
                 .WithOne(b => b.Payment)
                 .HasForeignKey<Booking>(b => b.PaymentId)
                 .OnDelete(DeleteBehavior.Restrict)
                 .IsRequired();
        });

        // ---- Booking ----
        _ = builder.Entity<Booking>(e =>
        {
            _ = e.HasKey(b => b.Id);
            _ = e.Property(b => b.IsDeleted).IsRequired();
            _ = e.HasMany(b => b.Flights)
                 .WithOne(bf => bf.Booking)
                 .HasForeignKey(bf => bf.BookingId)
                 .OnDelete(DeleteBehavior.Restrict);
            _ = e.HasMany(b => b.Passengers)
                 .WithOne(bp => bp.Booking)
                 .HasForeignKey(bp => bp.BookingId)
                 .OnDelete(DeleteBehavior.Restrict);
            _ = e.HasOne(b => b.Payment)
                 .WithOne(p => p.Booking)
                 .HasForeignKey<Booking>(b => b.PaymentId)
                 .OnDelete(DeleteBehavior.Restrict)
                 .IsRequired();
        });

        // ---- Service hierarchy ----
        _ = builder.Entity<Service>(e =>
        {
            _ = e.HasKey(s => s.Id);
            _ = e.HasDiscriminator<string>("ServiceType")
                 .HasValue<AirportCheckIn>(nameof(AirportCheckIn))
                 .HasValue<Baggage>(nameof(Baggage))
                 .HasValue<Flexibility>(nameof(Flexibility))
                 .HasValue<OnTimeArrival>(nameof(OnTimeArrival))
                 .HasValue<Priority>(nameof(Priority))
                 .HasValue<Seat>(nameof(Seat));
            _ = e.Property(s => s.Price).IsRequired().HasPrecision(18, 2);
            _ = e.Property(s => s.Name).IsRequired().HasMaxLength(100);
            _ = e.HasMany(s => s.Passengers)
                 .WithOne(ps => ps.Service)
                 .HasForeignKey(ps => ps.ServiceId)
                 .OnDelete(DeleteBehavior.Restrict);
        });

        // ---- PassengerService ----
        _ = builder.Entity<PassengerService>(e =>
        {
            _ = e.HasKey(ps => new { ps.ServiceId, ps.PassengerId });
            _ = e.HasOne(ps => ps.Passenger)
                 .WithMany(p => p.Services)
                 .HasForeignKey(ps => ps.PassengerId)
                 .OnDelete(DeleteBehavior.Restrict);
            _ = e.HasOne(ps => ps.Service)
                 .WithMany(s => s.Passengers)
                 .HasForeignKey(ps => ps.ServiceId)
                 .OnDelete(DeleteBehavior.Restrict);
        });

        // ---- TravelDocument ----
        _ = builder.Entity<TravelDocument>(e =>
        {
            _ = e.HasKey(td => td.Id);
            _ = e.Property(td => td.Number)
                 .IsRequired()
                 .HasMaxLength(50);
            _ = e.Property(td => td.IssueDate).IsRequired();
            _ = e.Property(td => td.ExpiryDate).IsRequired();
            _ = e.Property(td => td.Type)
                 .IsRequired()
                 .HasConversion<string>();
            _ = e.Property(td => td.Gender)
                 .IsRequired()
                 .HasConversion<string>();
            _ = e.HasOne(td => td.Nationality)
                 .WithMany(c => c.DocumentsNationalities)
                 .HasForeignKey(td => td.NationalityId)
                 .OnDelete(DeleteBehavior.Restrict)
                 .IsRequired();
            _ = e.HasOne(td => td.BirthCountry)
                 .WithMany(c => c.DocumentsBirthCountries)
                 .HasForeignKey(td => td.BirthCountryId)
                 .OnDelete(DeleteBehavior.Restrict)
                 .IsRequired();
            _ = e.HasOne(td => td.Passenger)
                 .WithOne(p => p.Document)
                 .HasForeignKey<TravelDocument>(td => td.PassengerId)
                 .OnDelete(DeleteBehavior.Restrict)
                 .IsRequired();
        });

        // ---- ChangeLog ----
        _ = builder.Entity<ChangeLog>(e =>
        {
            _ = e.HasKey(cl => cl.Id);
            _ = e.Property(cl => cl.EntityName)
                 .IsRequired()
                 .HasMaxLength(100);
            _ = e.Property(cl => cl.EntityId)
                 .IsRequired()
                 .HasMaxLength(450);
            _ = e.Property(cl => cl.UserId)
                 .IsRequired()
                 .HasMaxLength(450);
            _ = e.Property(cl => cl.Action)
                 .IsRequired()
                 .HasConversion<string>();
            _ = e.Property(cl => cl.TimestampUTC).IsRequired();
            _ = e.Property(cl => cl.BeforeJSON);
            _ = e.Property(cl => cl.AfterJSON);
        });
    }
}
