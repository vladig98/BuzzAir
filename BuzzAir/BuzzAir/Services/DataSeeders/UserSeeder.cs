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
            Street = secrets.Street
        };

        _ = await userManager.CreateAsync(user);
        _ = await userManager.AddPasswordAsync(user, secrets.Password);
        _ = await userManager.AddToRolesAsync(user, [GlobalConstants.ADMIN_ROLE]);

        _ = await userManager.SetEmailAsync(user, secrets.Email);
        _ = await userManager.SetPhoneNumberAsync(user, secrets.PhoneNumber);
        _ = await userManager.SetUserNameAsync(user, secrets.UserName);
    }
}
