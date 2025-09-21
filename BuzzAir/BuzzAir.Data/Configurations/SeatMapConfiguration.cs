namespace BuzzAir.Data.Configurations;
internal sealed class SeatMapConfiguration : IEntityTypeConfiguration<SeatMap>
{
    public void Configure(EntityTypeBuilder<SeatMap> builder)
    {
        _ = builder.HasKey(x => new { x.AircraftId, x.SeatNumber });
        _ = builder.HasOne(x => x.Aircraft)
                   .WithMany(x => x.SeatMap)
                   .HasForeignKey(x => x.AircraftId)
                   .OnDelete(DeleteBehavior.Restrict);
        _ = builder.Property(x => x.SeatType)
                   .HasConversion<string>();
    }
}
