namespace BuzzAir.Data.Configurations;
internal sealed class AircraftConfiguration : IEntityTypeConfiguration<Aircraft>
{
    public void Configure(EntityTypeBuilder<Aircraft> builder)
    {
        _ = builder.HasKey(a => a.Id);
        _ = builder.Property(a => a.Id).HasMaxLength(450).IsRequired();
        _ = builder.Property(a => a.Name)
                   .IsRequired()
                   .HasMaxLength(100);
        _ = builder.Property(a => a.NumberOfSeats).IsRequired();
        _ = builder.Property(a => a.IsDeleted).IsRequired();
        _ = builder.HasMany(a => a.Flights)
                   .WithOne(f => f.Aircraft)
                   .HasForeignKey(f => f.AircraftId)
                   .OnDelete(DeleteBehavior.Restrict);
    }
}
