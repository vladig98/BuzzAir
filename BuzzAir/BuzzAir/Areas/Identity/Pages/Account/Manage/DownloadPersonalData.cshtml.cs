// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
namespace BuzzAir.Areas.Identity.Pages.Account.Manage;

public sealed class DownloadPersonalDataModel(
    UserManager<ApplicationUser> userManager,
    ILogger<DownloadPersonalDataModel> logger) : PageModel
{
    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        ApplicationUser? user = await userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
        }

        logger.LogInformation("User with ID '{UserId}' asked for their personal data.", userManager.GetUserId(User));

        // Only include personal data for download
        Dictionary<string, string> personalData = [];
        IEnumerable<PropertyInfo> personalDataProps = typeof(ApplicationUser).GetProperties().Where(
                        prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));
        foreach (PropertyInfo p in personalDataProps)
        {
            personalData.Add(p.Name, p.GetValue(user)?.ToString() ?? "null");
        }

        IList<UserLoginInfo> logins = await userManager.GetLoginsAsync(user);
        foreach (UserLoginInfo l in logins)
        {
            personalData.Add($"{l.LoginProvider} external login provider key", l.ProviderKey);
        }

        personalData.Add($"Authenticator Key", await userManager.GetAuthenticatorKeyAsync(user) ?? string.Empty);

        _ = Response.Headers.TryAdd("Content-Disposition", "attachment; filename=PersonalData.json");
        return new FileContentResult(System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(personalData), "application/json");
    }
}
