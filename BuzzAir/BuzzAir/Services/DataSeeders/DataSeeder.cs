namespace BuzzAir.Services.DataSeeders;

public class DataSeeder(IServiceProvider serviceProvider) : IDataSeeder
{
    public async Task SeedAsync()
    {
        using IServiceScope scope = serviceProvider.CreateScope();
        BuzzAirDbContext dbContext = scope.ServiceProvider.GetRequiredService<BuzzAirDbContext>();

        if (dbContext.Database.IsRelational())
        {
            await dbContext.Database.MigrateAsync();
        }

        // Can't run them in parallel due to the dbContext limitation
        await RunSeeder(scope, GlobalConstants.COUNTRY_SEEDER);
        await RunSeeder(scope, GlobalConstants.TIMEZONE_SEEDER);
        await RunSeeder(scope, GlobalConstants.AIRCRAFT_SEEDER);
        await RunSeeder(scope, GlobalConstants.CITY_SEEDER);
        await RunSeeder(scope, GlobalConstants.AIRPORT_SEEDER);
        await RunSeeder(scope, GlobalConstants.FLIGHTS_SEEDER);
        await RunSeeder(scope, GlobalConstants.SERVICES_SEEDER);
        await RunSeeder(scope, GlobalConstants.ROLE_SEEDER);
        await RunSeeder(scope, GlobalConstants.USER_SEEDER);
        await RunSeeder(scope, GlobalConstants.SEAT_MAP_SEEDER);
    }

    private static async Task RunSeeder(IServiceScope scope, string seederName)
    {
        IDataSeeder seeder = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>(seederName);
        await seeder.SeedAsync();
    }
}
