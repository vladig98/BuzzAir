// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
namespace BuzzAir.Areas.Identity.Pages.Account;

internal class LoginWithRecoveryCodeModel(
    SignInManager<ApplicationUser> signInManager,
    UserManager<ApplicationUser> userManager,
    ILogger<LoginWithRecoveryCodeModel> logger) : PageModel
{

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    [BindProperty]
    public LoginWithRecoveryCodeInputModel Input { get; set; } = null!;

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public string ReturnUrl { get; set; } = string.Empty;

    public async Task<IActionResult> OnGetAsync(string? returnUrl = null)
    {
        // Ensure the user has gone through the username & password screen first
        _ = await signInManager.GetTwoFactorAuthenticationUserAsync() 
            ?? throw new InvalidOperationException($"Unable to load two-factor authentication user."); ;

        ReturnUrl = returnUrl ?? string.Empty;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        ApplicationUser user = await signInManager.GetTwoFactorAuthenticationUserAsync() 
            ?? throw new InvalidOperationException($"Unable to load two-factor authentication user.");

        string recoveryCode = Input.RecoveryCode.Replace(" ", string.Empty, StringComparison.Ordinal);
        Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.TwoFactorRecoveryCodeSignInAsync(recoveryCode);

        _ = await userManager.GetUserIdAsync(user);

        if (result.Succeeded)
        {
            logger.LogInformation("User with ID '{UserId}' logged in with a recovery code.", user.Id);
            return LocalRedirect(returnUrl ?? Url.Content("~/"));
        }
        if (result.IsLockedOut)
        {
            logger.LogWarning("User account locked out.");
            return RedirectToPage("./Lockout");
        }
        else
        {
            logger.LogWarning("Invalid recovery code entered for user with ID '{UserId}' ", user.Id);
            ModelState.AddModelError(string.Empty, "Invalid recovery code entered.");
            return Page();
        }
    }
}
