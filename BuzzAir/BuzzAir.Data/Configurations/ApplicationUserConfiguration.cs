namespace BuzzAir.Data.Configurations;
internal sealed class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        _ = builder.Property(a => a.Id).HasMaxLength(450).IsRequired();
        _ = builder.HasOne(u => u.Passenger)
                   .WithOne(p => p.User)
                   .HasForeignKey<ApplicationUser>(u => u.PassengerId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(false);
        _ = builder.HasOne(u => u.City)
                   .WithMany(c => c.Users)
                   .HasForeignKey(u => u.CityId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
        _ = builder.Property(u => u.FirstName).IsRequired().HasMaxLength(50);
        _ = builder.Property(u => u.LastName).IsRequired().HasMaxLength(50);
        _ = builder.Property(u => u.PostalCode).IsRequired().HasMaxLength(20);
        _ = builder.Property(u => u.Street).IsRequired().HasMaxLength(150);
        _ = builder.Property(u => u.Gender)
                   .IsRequired()
                   .HasConversion<string>();
        _ = builder.Property(u => u.DateOfBirth)
                   .IsRequired();
    }
}
