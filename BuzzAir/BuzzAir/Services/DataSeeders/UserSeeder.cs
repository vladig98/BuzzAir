using StackExchange.Redis;

namespace BuzzAir.Services.DataSeeders;

public class UserSeeder(
    UserManager<ApplicationUser> userManager,
    IConfiguration configuration,
    BuzzAirDbContext dbContext) : IDataSeeder
{
    private const string _dateFormat = "yyyy-MM-dd";
    private const string _configKey = "DataSeed:Admin";

    public async Task SeedAsync()
    {
        if (await userManager.Users.AnyAsync())
        {
            return;
        }

        SeedingDataSecrets secrets = new();
        configuration.GetSection(_configKey).Bind(secrets);

        Dictionary<string, City> cities = await dbContext.Cities
            .Include(x => x.State)
            .Include(x => x.Country)
            .AsNoTracking()
            .ToDictionaryAsync(x => $"{x.Name}__{x.State?.Name}__{x.Country.Name}", x => x);

        string key = $"{secrets.CityName}__{secrets.StateName}__{secrets.CountryName}";
        ApplicationUser user = new()
        {
            CityId = cities.TryGetValue(key, out City? city) ? city!.Id : cities.First().Value.Id,
            DateOfBirth = DateTime.SpecifyKind(DateTime.ParseExact(secrets.DOB, _dateFormat, CultureInfo.InvariantCulture), DateTimeKind.Utc),
            FirstName = secrets.FirstName,
            LastName = secrets.LastName,
            Gender = Enum.Parse<Gender>(secrets.Gender),
            PostalCode = secrets.PostalCode,
            Street = secrets.Street,
            Email = secrets.Email,
            PhoneNumber = secrets.PhoneNumber,
            UserName = secrets.UserName,
            Id = Guid.NewGuid().ToString()
        };

        _ = await userManager.CreateAsync(user, secrets.Password);

        _ = await userManager.AddToRoleAsync(user, GlobalConstants.ADMIN_ROLE);
        _ = await userManager.AddClaimAsync(user, claim: new Claim(ClaimTypes.Role.ToString(), GlobalConstants.ADMIN_ROLE));
    }
}
