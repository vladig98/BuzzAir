namespace BuzzAir.Data.Configurations;
internal sealed class TravelDocumentConfiguration : IEntityTypeConfiguration<TravelDocument>
{
    public void Configure(EntityTypeBuilder<TravelDocument> builder)
    {
        _ = builder.HasKey(td => td.Id);
        _ = builder.Property(a => a.Id).HasMaxLength(450).IsRequired();
        _ = builder.Property(td => td.Number)
                   .IsRequired()
                   .HasMaxLength(50);
        _ = builder.Property(td => td.IssueDate).IsRequired();
        _ = builder.Property(td => td.ExpiryDate).IsRequired();
        _ = builder.Property(td => td.Type)
                   .IsRequired()
                   .HasConversion<string>();
        _ = builder.Property(td => td.Gender)
                   .IsRequired()
                   .HasConversion<string>();
        _ = builder.HasOne(td => td.Nationality)
                   .WithMany(c => c.DocumentsNationalities)
                   .HasForeignKey(td => td.NationalityId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
        _ = builder.HasOne(td => td.BirthCountry)
                   .WithMany(c => c.DocumentsBirthCountries)
                   .HasForeignKey(td => td.BirthCountryId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
        _ = builder.HasOne(td => td.Passenger)
                   .WithOne(p => p.Document)
                   .HasForeignKey<TravelDocument>(td => td.PassengerId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
    }
}
