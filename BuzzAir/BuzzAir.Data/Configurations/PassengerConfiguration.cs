namespace BuzzAir.Data.Configurations;
internal sealed class PassengerConfiguration : IEntityTypeConfiguration<Passenger>
{
    public void Configure(EntityTypeBuilder<Passenger> builder)
    {
        _ = builder.HasKey(p => p.Id);
        _ = builder.Property(a => a.Id).HasMaxLength(450).IsRequired();
        _ = builder.Property(p => p.FirstName)
                   .IsRequired()
                   .HasMaxLength(50);
        _ = builder.Property(p => p.LastName)
                   .IsRequired()
                   .HasMaxLength(50);
        _ = builder.Property(p => p.DateOfBirth).IsRequired();
        _ = builder.Property(p => p.Gender).IsRequired();
        _ = builder.HasOne(p => p.Document)
                   .WithOne(d => d.Passenger)
                   .HasForeignKey<TravelDocument>(d => d.PassengerId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
        _ = builder.HasOne(p => p.User)
                   .WithOne(u => u.Passenger)
                   .HasForeignKey<ApplicationUser>(u => u.PassengerId)
                   .OnDelete(DeleteBehavior.Restrict);
        _ = builder.HasMany(p => p.Services)
                   .WithOne(ps => ps.Passenger)
                   .HasForeignKey(ps => ps.PassengerId)
                   .OnDelete(DeleteBehavior.Restrict);
        _ = builder.HasMany(p => p.Flights)
                   .WithOne(fp => fp.Passenger)
                   .HasForeignKey(fp => fp.PassengerId)
                   .OnDelete(DeleteBehavior.Restrict);
        _ = builder.HasMany(p => p.Bookings)
                   .WithOne(bp => bp.Passenger)
                   .HasForeignKey(bp => bp.PassengerId)
                   .OnDelete(DeleteBehavior.Restrict);
    }
}
