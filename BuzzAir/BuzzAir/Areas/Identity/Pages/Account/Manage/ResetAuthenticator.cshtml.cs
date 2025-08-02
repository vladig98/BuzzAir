// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
namespace BuzzAir.Areas.Identity.Pages.Account.Manage;

#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable CA1515 // Consider making public types internal
public sealed class ResetAuthenticatorModel(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    ILogger<ResetAuthenticatorModel> logger) : PageModel
#pragma warning restore CA1515 // Consider making public types internal
#pragma warning restore IDE0079 // Remove unnecessary suppression
{
    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    [TempData]
    public string StatusMessage { get; set; } = string.Empty;

    public async Task<IActionResult> OnGet()
    {
        ApplicationUser? user = await userManager.GetUserAsync(User);
        return user == null ? NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.") : Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        ApplicationUser? user = await userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
        }

        _ = await userManager.SetTwoFactorEnabledAsync(user, false);
        _ = await userManager.ResetAuthenticatorKeyAsync(user);
        _ = await userManager.GetUserIdAsync(user);
        logger.LogInformation("User with ID '{UserId}' has reset their authentication app key.", user.Id);

        await signInManager.RefreshSignInAsync(user);
        StatusMessage = "Your authenticator app key has been reset, you will need to configure your authenticator app using the new key.";

        return RedirectToPage("./EnableAuthenticator");
    }
}
