namespace BuzzAir.Data.Configurations;
internal sealed class FlightConfiguration : IEntityTypeConfiguration<Flight>
{
    public void Configure(EntityTypeBuilder<Flight> builder)
    {
        _ = builder.HasKey(f => f.Id);
        _ = builder.Property(f => f.FlightNumber)
                   .IsRequired()
                   .HasMaxLength(10);
        _ = builder.Property(f => f.DepartureUTC).IsRequired();
        _ = builder.Property(f => f.ArrivalUTC).IsRequired();
        _ = builder.Property(f => f.PriceInEur)
                   .IsRequired()
                   .HasPrecision(18, 2);
        _ = builder.Property(f => f.IsDeleted).IsRequired();
        _ = builder.HasOne(f => f.Origin)
                   .WithMany(a => a.FlightsFrom)
                   .HasForeignKey(f => f.OriginId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
        _ = builder.HasOne(f => f.Destination)
                   .WithMany(a => a.FlightsTo)
                   .HasForeignKey(f => f.DestinationId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
        _ = builder.HasOne(f => f.Aircraft)
                   .WithMany(a => a.Flights)
                   .HasForeignKey(f => f.AircraftId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
        _ = builder.HasMany(f => f.Passengers)
                   .WithOne(fp => fp.Flight)
                   .HasForeignKey(fp => fp.FlightId)
                   .OnDelete(DeleteBehavior.Restrict);
        _ = builder.HasMany(f => f.Bookings)
                   .WithOne(bf => bf.Flight)
                   .HasForeignKey(bf => bf.FlightId)
                   .OnDelete(DeleteBehavior.Restrict);
        _ = builder.Property(f => f.TakenSeats)
                   .IsRequired()
                   .HasMaxLength(2000);
    }
}
