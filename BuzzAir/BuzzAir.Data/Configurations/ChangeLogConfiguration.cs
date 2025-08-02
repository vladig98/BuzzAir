namespace BuzzAir.Data.Configurations;
internal sealed class ChangeLogConfiguration : IEntityTypeConfiguration<ChangeLog>
{
    public void Configure(EntityTypeBuilder<ChangeLog> builder)
    {
        _ = builder.HasKey(cl => cl.Id);
        _ = builder.Property(cl => cl.EntityName)
                   .IsRequired()
                   .HasMaxLength(100);
        _ = builder.Property(cl => cl.EntityId)
                   .IsRequired()
                   .HasMaxLength(450);
        _ = builder.Property(cl => cl.UserId)
                   .IsRequired()
                   .HasMaxLength(450);
        _ = builder.Property(cl => cl.Action)
                   .IsRequired()
                   .HasConversion<string>();
        _ = builder.Property(cl => cl.TimestampUTC).IsRequired();
        _ = builder.Property(cl => cl.BeforeJSON);
        _ = builder.Property(cl => cl.AfterJSON);
    }
}
