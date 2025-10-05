namespace BuzzAir.Data.Configurations;
internal sealed class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        _ = builder.HasKey(c => c.Id);
        _ = builder.Property(a => a.Id).HasMaxLength(450).IsRequired();
        _ = builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100);
        _ = builder.Property(c => c.ISOA2)
                   .IsRequired()
                   .HasMaxLength(2);
        _ = builder.Property(c => c.ISOA3)
                   .IsRequired()
                   .HasMaxLength(3);
        _ = builder.Property(c => c.IsOfficiallyRecognizedCountry).IsRequired();
        _ = builder.Property(c => c.IsDeleted).IsRequired();
        _ = builder.HasMany(c => c.Cities)
                   .WithOne(ci => ci.Country)
                   .HasForeignKey(ci => ci.CountryId)
                   .OnDelete(DeleteBehavior.Restrict);
        _ = builder.HasMany(c => c.States)
                   .WithOne(st => st.Country)
                   .HasForeignKey(st => st.CountryId)
                   .OnDelete(DeleteBehavior.Restrict);
        _ = builder.HasMany(c => c.DocumentsNationalities)
                   .WithOne(td => td.Nationality)
                   .HasForeignKey(td => td.NationalityId)
                   .OnDelete(DeleteBehavior.Restrict);
        _ = builder.HasMany(c => c.DocumentsBirthCountries)
                   .WithOne(td => td.BirthCountry)
                   .HasForeignKey(td => td.BirthCountryId)
                   .OnDelete(DeleteBehavior.Restrict);
    }
}
