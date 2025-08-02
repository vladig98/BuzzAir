namespace BuzzAir.Data.Configurations;
internal sealed class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        _ = builder.HasKey(b => b.Id);
        _ = builder.Property(b => b.IsDeleted).IsRequired();
        _ = builder.HasMany(b => b.Flights)
                   .WithOne(bf => bf.Booking)
                   .HasForeignKey(bf => bf.BookingId)
                   .OnDelete(DeleteBehavior.Restrict);
        _ = builder.HasMany(b => b.Passengers)
                   .WithOne(bp => bp.Booking)
                   .HasForeignKey(bp => bp.BookingId)
                   .OnDelete(DeleteBehavior.Restrict);
        _ = builder.HasOne(b => b.Payment)
                   .WithOne(p => p.Booking)
                   .HasForeignKey<Booking>(b => b.PaymentId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
    }
}
