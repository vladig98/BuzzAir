namespace BuzzAir.Data.Configurations;
internal sealed class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        _ = builder.HasKey(c => c.Id);
        _ = builder.Property(a => a.Id).HasMaxLength(450).IsRequired();
        _ = builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100);
        _ = builder.Property(c => c.IsDeleted).IsRequired();
        _ = builder.HasOne(c => c.Country)
                   .WithMany(cn => cn.Cities)
                   .HasForeignKey(c => c.CountryId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
        _ = builder.HasOne(c => c.State)
                   .WithMany(s => s.Cities)
                   .HasForeignKey(c => c.StateId)
                   .OnDelete(DeleteBehavior.Restrict);
        _ = builder.HasOne(c => c.Timezone)
                   .WithMany(tz => tz.Cities)
                   .HasForeignKey(c => c.TimezoneId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
        _ = builder.HasMany(c => c.Airports)
                   .WithOne(a => a.City)
                   .HasForeignKey(a => a.CityId)
                   .OnDelete(DeleteBehavior.Restrict);
        _ = builder.HasMany(c => c.Users)
                   .WithOne(u => u.City)
                   .HasForeignKey(u => u.CityId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
    }
}
