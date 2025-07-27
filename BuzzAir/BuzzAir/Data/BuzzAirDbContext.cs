namespace BuzzAir.Data
{
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

        public BuzzAirDbContext()
        {
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
                .Where(e =>
                    e.State == EntityState.Modified ||
                    e.State == EntityState.Added ||
                    e.State == EntityState.Deleted);

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

                ChangeLogs.Add(changeLog);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            IEnumerable<IMutableProperty> decimalProps = builder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => (Nullable.GetUnderlyingType(p.ClrType) ?? p.ClrType) == typeof(decimal));

            foreach (IMutableProperty property in decimalProps)
            {
                property.SetPrecision(29);
                property.SetScale(28);
            }

            builder.Entity<ChangeLog>(x =>
            {
                x.Property(cl => cl.Action).HasConversion<string>();
            });

            builder.Entity<Flight>(x =>
            {
                x.Property(fl => fl.TakenSeats)
                    .HasComputedColumnSql("(SELECT COUNT(*) FROM FlightPassenger WHERE FlightId = Id)", stored: true)
                    .ValueGeneratedOnAddOrUpdate()
                    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

                x.HasOne(fl => fl.Origin).WithMany(o => o.FlightsFrom).HasForeignKey(fl => fl.OriginId).OnDelete(DeleteBehavior.Restrict);
                x.HasOne(fl => fl.Destination).WithMany(o => o.FlightsTo).HasForeignKey(fl => fl.DestinationId).OnDelete(DeleteBehavior.Restrict);
                x.HasOne(fl => fl.Aircraft).WithMany(a => a.Flights).HasForeignKey(fl => fl.AircraftId).OnDelete(DeleteBehavior.Restrict);
                x.HasIndex(fl => fl.FlightNumber).IsUnique(false);
            });

            builder.Entity<FlightPassenger>(x =>
            {
                x.HasKey(fp => new { fp.FlightId, fp.PassengerId, fp.SeatNumber });
                x.HasOne(fp => fp.Flight).WithMany(f => f.Passengers).HasForeignKey(fp => fp.FlightId).OnDelete(DeleteBehavior.Restrict);
                x.HasOne(fp => fp.Passenger).WithMany(p => p.Flights).HasForeignKey(fp => fp.PassengerId).OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Airport>(x =>
            {
                x.HasOne(a => a.City).WithMany(c => c.Airports).HasForeignKey(a => a.CityId).OnDelete(DeleteBehavior.Restrict);
                x.HasIndex(a => a.ICAO).IsUnique();
                x.HasIndex(a => a.IATA).IsUnique();
            });

            builder.Entity<City>(x =>
            {
                x.HasOne(c => c.State).WithMany(s => s.Cities).HasForeignKey(c => c.StateId).OnDelete(DeleteBehavior.Restrict);
                x.HasOne(c => c.Country).WithMany(c => c.Cities).HasForeignKey(c => c.CountryId).OnDelete(DeleteBehavior.Restrict);
                x.HasOne(c => c.Timezone).WithMany(t => t.Cities).HasForeignKey(c => c.TimezoneId).OnDelete(DeleteBehavior.Restrict);
                x.HasIndex(c => new { c.Name, c.StateId }).IsUnique();
            });

            builder.Entity<State>(x =>
            {
                x.HasOne(s => s.Country).WithMany(c => c.States).HasForeignKey(s => s.CountryId).OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Country>(x =>
            {
                x.HasIndex(c => c.ISO).IsUnique();
                x.HasIndex(c => c.Name).IsUnique();
            });

            builder.Entity<ApplicationUser>(x =>
            {
                x.Property(au => au.Gender).HasConversion<string>();
                x.HasOne(au => au.Passenger).WithOne(p => p.User).HasForeignKey<ApplicationUser>(au => au.PassengerId).OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<TravelDocument>(x =>
            {
                x.Property(td => td.Type).HasConversion<string>();
                x.Property(td => td.Gender).HasConversion<string>();
                x.HasOne(td => td.BirthCountry).WithMany(c => c.DocumentsBirthCountries).HasForeignKey(td => td.BirthCountryId).OnDelete(DeleteBehavior.Restrict);
                x.HasOne(td => td.Nationality).WithMany(c => c.DocumentsNationalities).HasForeignKey(td => td.NationalityId).OnDelete(DeleteBehavior.Restrict);
                x.HasOne(td => td.Passenger).WithOne(p => p.Document).HasForeignKey<TravelDocument>(td => td.PassengerId).OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<BookingPassenger>(x =>
            {
                x.HasKey(bp => new { bp.PassengerId, bp.BookingId });
                x.HasOne(bp => bp.Passenger).WithMany(p => p.Bookings).HasForeignKey(bp => bp.PassengerId).OnDelete(DeleteBehavior.Restrict);
                x.HasOne(bp => bp.Booking).WithMany(p => p.Passengers).HasForeignKey(bp => bp.BookingId).OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<BookingFlight>(x =>
            {
                x.HasKey(bf => new { bf.FlightId, bf.BookingId });
                x.HasOne(bf => bf.Flight).WithMany(f => f.Bookings).HasForeignKey(bf => bf.FlightId).OnDelete(DeleteBehavior.Restrict);
                x.HasOne(bf => bf.Booking).WithMany(f => f.Flights).HasForeignKey(bf => bf.BookingId).OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Booking>(x =>
            {
                x.HasOne(b => b.Payment).WithOne(x => x.Booking).HasForeignKey<Booking>(b => b.PaymentId).OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Payment>(x =>
            {
                x.Property(p => p.Card).HasConversion<string>();
                x.HasIndex(p => p.BookingId).IsUnique();
            });

            builder.Entity<Passenger>(x =>
            {
                x.HasIndex(p => p.UserId).IsUnique();
            });

            builder.Entity<PassengerService>(x =>
            {
                x.HasKey(ps => new { ps.ServiceId, ps.PassengerId });
                x.HasOne(ps => ps.Passenger).WithMany(p => p.Services).HasForeignKey(ps => ps.PassengerId).OnDelete(DeleteBehavior.Restrict);
                x.HasOne(ps => ps.Service).WithMany(p => p.Passengers).HasForeignKey(ps => ps.ServiceId).OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Seat>(x =>
            {
                x.Property(s => s.SeatType).HasConversion<string>();
            });

            builder.Entity<Baggage>(x =>
            {
                x.Property(b => b.BaggageType).HasConversion<string>();
                x.Property(b => b.Price).HasComputedColumnSql(
                    @$"CASE 
                        WHEN BaggageType = '{BaggageType.TwentyKilos}' 
                        THEN {GlobalConstants.PriceFor20kg} 
                        WHEN BaggageType = '{BaggageType.ThirtyTwoKilos}' 
                        THEN {GlobalConstants.PriceFor32kg} 
                        ELSE {GlobalConstants.PriceForCabin} 
                    END", stored: true)
                .ValueGeneratedOnAddOrUpdate()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

                x.Property(b => b.Kilos).HasComputedColumnSql(
                    @$"CASE 
                        WHEN BaggageType = '{BaggageType.TwentyKilos}' 
                        THEN {GlobalConstants.TwentyKilos} 
                        WHEN BaggageType = '{BaggageType.ThirtyTwoKilos}' 
                        THEN {GlobalConstants.ThrityTwoKilos} 
                        ELSE {GlobalConstants.CabinKilos} 
                    END", stored: true)
                .ValueGeneratedOnAddOrUpdate()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            });

            builder.Entity<Service>()
                .HasDiscriminator<string>("ServiceType")
                .HasValue<AirportCheckIn>(nameof(AirportCheckIn))
                .HasValue<Baggage>(nameof(Baggage))
                .HasValue<Flexibility>(nameof(Flexibility))
                .HasValue<OnTimeArrival>(nameof(OnTimeArrival))
                .HasValue<Priority>(nameof(Priority))
                .HasValue<Seat>(nameof(Seat));
        }
    }
}
