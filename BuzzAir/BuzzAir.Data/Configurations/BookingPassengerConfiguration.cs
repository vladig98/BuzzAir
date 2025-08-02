namespace BuzzAir.Data.Configurations;
internal sealed class BookingPassengerConfiguration : IEntityTypeConfiguration<BookingPassenger>
{
    public void Configure(EntityTypeBuilder<BookingPassenger> builder)
    {
        _ = builder.HasKey(bp => new { bp.PassengerId, bp.BookingId });
        _ = builder.HasOne(bp => bp.Booking)
                   .WithMany(b => b.Passengers)
                   .HasForeignKey(bp => bp.BookingId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
        _ = builder.HasOne(bp => bp.Passenger)
                   .WithMany(p => p.Bookings)
                   .HasForeignKey(bp => bp.PassengerId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
    }
}
