namespace BuzzAir.Data.Configurations;
internal sealed class StateConfiguration : IEntityTypeConfiguration<State>
{
    public void Configure(EntityTypeBuilder<State> builder)
    {
        _ = builder.HasKey(s => s.Id);
        _ = builder.Property(s => s.Name)
                   .IsRequired()
                   .HasMaxLength(100);
        _ = builder.Property(s => s.IsDeleted).IsRequired();
        _ = builder.HasOne(s => s.Country)
                   .WithMany(c => c.States)
                   .HasForeignKey(s => s.CountryId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
        _ = builder.HasMany(s => s.Cities)
                   .WithOne(c => c.State)
                   .HasForeignKey(c => c.StateId)
                   .OnDelete(DeleteBehavior.Restrict);
    }
}
