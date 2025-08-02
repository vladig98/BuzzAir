namespace BuzzAir.Data.Configurations;
internal sealed class AirportConfiguration : IEntityTypeConfiguration<Airport>
{
    public void Configure(EntityTypeBuilder<Airport> builder)
    {
        _ = builder.HasKey(a => a.Id);
        _ = builder.Property(a => a.Id).HasMaxLength(450).IsRequired();
        _ = builder.Property(a => a.ICAO)
                   .IsRequired()
                   .HasMaxLength(4);
        _ = builder.Property(a => a.IATA)
                   .IsRequired()
                   .HasMaxLength(3);
        _ = builder.Property(a => a.Name)
                   .IsRequired()
                   .HasMaxLength(150);
        _ = builder.Property(a => a.IsDeleted).IsRequired();
        _ = builder.HasOne(a => a.City)
                   .WithMany(c => c.Airports)
                   .HasForeignKey(a => a.CityId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
        _ = builder.HasMany(a => a.FlightsFrom)
                   .WithOne(f => f.Origin)
                   .HasForeignKey(f => f.OriginId)
                   .OnDelete(DeleteBehavior.Restrict);
        _ = builder.HasMany(a => a.FlightsTo)
                   .WithOne(f => f.Destination)
                   .HasForeignKey(f => f.DestinationId)
                   .OnDelete(DeleteBehavior.Restrict);
        // optional coords
        _ = builder.Property(a => a.Latitude).HasPrecision(9, 6);
        _ = builder.Property(a => a.Longitude).HasPrecision(9, 6);
        _ = builder.Property(a => a.ElevationAboveSeaLevel);
    }
}
