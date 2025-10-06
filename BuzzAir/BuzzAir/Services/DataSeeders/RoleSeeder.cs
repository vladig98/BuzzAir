namespace BuzzAir.Services.DataSeeders;

public class RoleSeeder(RoleManager<IdentityRole> roleManager) : IDataSeeder
{
    public async Task SeedAsync()
    {
        if (await roleManager.Roles.AnyAsync())
        {
            return;
        }

        _ = await roleManager.CreateAsync(new IdentityRole() { Name = GlobalConstants.ADMIN_ROLE });
        _ = await roleManager.CreateAsync(new IdentityRole() { Name = GlobalConstants.USER_ROLE });
    }
}
