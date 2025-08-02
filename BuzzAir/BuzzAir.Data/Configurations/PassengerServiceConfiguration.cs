namespace BuzzAir.Data.Configurations;
internal sealed class PassengerServiceConfiguration : IEntityTypeConfiguration<PassengerService>
{
    public void Configure(EntityTypeBuilder<PassengerService> builder)
    {
        _ = builder.HasKey(ps => new { ps.ServiceId, ps.PassengerId });
        _ = builder.HasOne(ps => ps.Passenger)
                   .WithMany(p => p.Services)
                   .HasForeignKey(ps => ps.PassengerId)
                   .OnDelete(DeleteBehavior.Restrict);
        _ = builder.HasOne(ps => ps.Service)
                   .WithMany(s => s.Passengers)
                   .HasForeignKey(ps => ps.ServiceId)
                   .OnDelete(DeleteBehavior.Restrict);
    }
}
