namespace BuzzAir.Data.Configurations;
internal sealed class FlightPassengerConfiguration : IEntityTypeConfiguration<FlightPassenger>
{
    public void Configure(EntityTypeBuilder<FlightPassenger> builder)
    {
        _ = builder.HasKey(fp => new { fp.FlightId, fp.PassengerId, fp.SeatNumber });
        _ = builder.HasOne(fp => fp.Flight)
                   .WithMany(f => f.Passengers)
                   .HasForeignKey(fp => fp.FlightId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
        _ = builder.HasOne(fp => fp.Passenger)
                   .WithMany(p => p.Flights)
                   .HasForeignKey(fp => fp.PassengerId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
        _ = builder.Property(fp => fp.SeatNumber).IsRequired();
    }
}
