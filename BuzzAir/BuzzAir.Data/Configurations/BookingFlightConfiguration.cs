namespace BuzzAir.Data.Configurations;
internal sealed class BookingFlightConfiguration : IEntityTypeConfiguration<BookingFlight>
{
    public void Configure(EntityTypeBuilder<BookingFlight> builder)
    {
        _ = builder.HasKey(bf => new { bf.FlightId, bf.BookingId });
        _ = builder.HasOne(bf => bf.Booking)
                   .WithMany(b => b.Flights)
                   .HasForeignKey(bf => bf.BookingId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
        _ = builder.HasOne(bf => bf.Flight)
                   .WithMany(f => f.Bookings)
                   .HasForeignKey(bf => bf.FlightId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
    }
}
