namespace BuzzAir.Data.Configurations;
internal sealed class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        _ = builder.HasKey(s => s.Id);
        _ = builder.Property(a => a.Id).HasMaxLength(450).IsRequired();
        _ = builder.HasDiscriminator<string>("ServiceType")
                   .HasValue<AirportCheckIn>(nameof(AirportCheckIn))
                   .HasValue<Baggage>(nameof(Baggage))
                   .HasValue<Flexibility>(nameof(Flexibility))
                   .HasValue<OnTimeArrival>(nameof(OnTimeArrival))
                   .HasValue<Priority>(nameof(Priority))
                   .HasValue<Seat>(nameof(Seat));
        _ = builder.Property(s => s.Price).IsRequired().HasPrecision(18, 2);
        _ = builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
        _ = builder.HasMany(s => s.Passengers)
                   .WithOne(ps => ps.Service)
                   .HasForeignKey(ps => ps.ServiceId)
                   .OnDelete(DeleteBehavior.Restrict);
    }
}
