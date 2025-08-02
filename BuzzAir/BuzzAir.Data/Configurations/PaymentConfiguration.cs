namespace BuzzAir.Data.Configurations;
internal sealed class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        _ = builder.HasKey(p => p.Id);
        _ = builder.Property(p => p.Card)
                   .IsRequired()
                   .HasConversion<string>();
        _ = builder.Property(p => p.ExpiryDate).IsRequired();
        _ = builder.Property(p => p.CardNumber)
                   .IsRequired()
                   .HasMaxLength(20);
        _ = builder.Property(p => p.CardHolder)
                   .IsRequired()
                   .HasMaxLength(100);
        _ = builder.Property(p => p.CVC)
                   .IsRequired()
                   .HasMaxLength(4);
        _ = builder.Property(p => p.AmountInEur)
                   .IsRequired()
                   .HasPrecision(18, 2);
        _ = builder.HasOne(p => p.Booking)
                   .WithOne(b => b.Payment)
                   .HasForeignKey<Booking>(b => b.PaymentId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
    }
}
