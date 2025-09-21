using BuzzAir.Data.Configurations;

namespace BuzzAir.Data.ExtensionMethods;

public static class ModelBuilderExtensions
{
    public static void AddConfiguration(this ModelBuilder builder)
    {
        if (builder is null)
        {
            return;
        }

        _ = builder.ApplyConfiguration(new AircraftConfiguration());
        _ = builder.ApplyConfiguration(new AirportConfiguration());
        _ = builder.ApplyConfiguration(new ApplicationUserConfiguration());
        _ = builder.ApplyConfiguration(new BookingConfiguration());
        _ = builder.ApplyConfiguration(new BookingFlightConfiguration());
        _ = builder.ApplyConfiguration(new BookingPassengerConfiguration());
        _ = builder.ApplyConfiguration(new ChangeLogConfiguration());
        _ = builder.ApplyConfiguration(new CityConfiguration());
        _ = builder.ApplyConfiguration(new CountryConfiguration());
        _ = builder.ApplyConfiguration(new FlightConfiguration());
        _ = builder.ApplyConfiguration(new FlightPassengerConfiguration());
        _ = builder.ApplyConfiguration(new PassengerConfiguration());
        _ = builder.ApplyConfiguration(new PassengerServiceConfiguration());
        _ = builder.ApplyConfiguration(new PaymentConfiguration());
        _ = builder.ApplyConfiguration(new ServiceConfiguration());
        _ = builder.ApplyConfiguration(new StateConfiguration());
        _ = builder.ApplyConfiguration(new TimezoneConfiguration());
        _ = builder.ApplyConfiguration(new TravelDocumentConfiguration());
        _ = builder.ApplyConfiguration(new SeatMapConfiguration());
    }
}
