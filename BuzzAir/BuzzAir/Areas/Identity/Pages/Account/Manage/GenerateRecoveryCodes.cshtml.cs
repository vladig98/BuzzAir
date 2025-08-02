// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
namespace BuzzAir.Areas.Identity.Pages.Account.Manage;

#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable CA1056 // URI-like properties should not be strings
#pragma warning disable CA1515 // Consider making public types internal
#pragma warning disable CA2227 // Collection properties should be read only
#pragma warning disable CA1819 // Properties should not return arrays
#pragma warning disable CA1054 // URI-like parameters should not be strings
public sealed class GenerateRecoveryCodesModel(
    UserManager<ApplicationUser> userManager,
    ILogger<GenerateRecoveryCodesModel> logger) : PageModel
{
    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    [TempData]
    public string[] RecoveryCodes { get; set; } = [];

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    [TempData]
    public string StatusMessage { get; set; } = string.Empty;

    public async Task<IActionResult> OnGetAsync()
    {
        ApplicationUser? user = await userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
        }

        bool isTwoFactorEnabled = await userManager.GetTwoFactorEnabledAsync(user);
        return !isTwoFactorEnabled
            ? throw new InvalidOperationException($"Cannot generate recovery codes for user because they do not have 2FA enabled.")
            : (IActionResult)Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        ApplicationUser? user = await userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
        }

        bool isTwoFactorEnabled = await userManager.GetTwoFactorEnabledAsync(user);
        string userId = await userManager.GetUserIdAsync(user);
        if (!isTwoFactorEnabled)
        {
            throw new InvalidOperationException($"Cannot generate recovery codes for user as they do not have 2FA enabled.");
        }

        IEnumerable<string>? recoveryCodes = await userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);
        RecoveryCodes = recoveryCodes?.ToArray() ?? [];

        logger.LogInformation("User with ID '{UserId}' has generated new 2FA recovery codes.", userId);
        StatusMessage = "You have generated new recovery codes.";
        return RedirectToPage("./ShowRecoveryCodes");
    }
}
#pragma warning restore CA1056 // URI-like properties should not be strings
#pragma warning restore CA1054 // URI-like parameters should not be strings
#pragma warning restore CA1819 // Properties should not return arrays
#pragma warning restore CA1819 // Properties should not return arrays
#pragma warning restore CA1515 // Consider making public types internal
#pragma warning restore IDE0079 // Remove unnecessary suppression
