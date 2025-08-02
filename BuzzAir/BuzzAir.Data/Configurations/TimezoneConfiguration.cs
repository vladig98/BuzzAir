namespace BuzzAir.Data.Configurations;
internal sealed class TimezoneConfiguration : IEntityTypeConfiguration<Timezone>
{
    public void Configure(EntityTypeBuilder<Timezone> builder)
    {
        _ = builder.HasKey(tz => tz.Id);
        _ = builder.Property(tz => tz.Name)
                   .IsRequired()
                   .HasMaxLength(100);
        _ = builder.Property(tz => tz.Identifier)
                   .IsRequired()
                   .HasMaxLength(100);
        _ = builder.Property(tz => tz.Abbreviation)
                   .IsRequired()
                   .HasMaxLength(10);
        _ = builder.Property(tz => tz.UsesDST).IsRequired();
        _ = builder.Property(tz => tz.IsDeleted).IsRequired();
        _ = builder.Property(tz => tz.Offset).IsRequired();
        _ = builder.HasMany(tz => tz.Cities)
                   .WithOne(c => c.Timezone)
                   .HasForeignKey(c => c.TimezoneId)
                   .OnDelete(DeleteBehavior.Restrict);
    }
}
