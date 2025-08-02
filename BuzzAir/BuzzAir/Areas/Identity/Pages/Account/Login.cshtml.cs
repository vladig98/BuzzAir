namespace BuzzAir.Areas.Identity.Pages.Account;

[AllowAnonymous]
#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable CA1056 // URI-like properties should not be strings
#pragma warning disable CA1515 // Consider making public types internal
#pragma warning disable CA1054 // URI-like parameters should not be strings
#pragma warning disable CA2227 // Collection properties should be read only
public sealed class LoginModel(SignInManager<ApplicationUser> signInManager, ILogger<LoginModel> logger) : PageModel
{
    [BindProperty]
    public LoginInputModel Input { get; set; } = null!;

    public IList<AuthenticationScheme> ExternalLogins { get; set; } = [];

    public string ReturnUrl { get; set; } = string.Empty;

    [TempData]
    public string ErrorMessage { get; set; } = string.Empty;

    public async Task OnGetAsync(string? returnUrl = null)
    {
        if (!string.IsNullOrEmpty(ErrorMessage))
        {
            ModelState.AddModelError(string.Empty, ErrorMessage);
        }

        returnUrl ??= Url.Content("~/");

        // Clear the existing external cookie to ensure a clean login process
        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        ExternalLogins = [.. await signInManager.GetExternalAuthenticationSchemesAsync()];
        ReturnUrl = returnUrl;
    }

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");

        if (ModelState.IsValid)
        {
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            Microsoft.AspNetCore.Identity.SignInResult result =
                await signInManager.PasswordSignInAsync(Input.Username, Input.Password, Input.RememberMe, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                logger.LogInformation("User logged in.");
                return LocalRedirect(returnUrl);
            }
            if (result.RequiresTwoFactor)
            {
                return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, Input.RememberMe });
            }
            if (result.IsLockedOut)
            {
                logger.LogWarning("User account locked out.");
                return RedirectToPage("./Lockout");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }
        }

        // If we got this far, something failed, redisplay form
        return Page();
    }
}
#pragma warning restore CA1056 // URI-like properties should not be strings
#pragma warning restore CA1054 // URI-like parameters should not be strings
#pragma warning restore CA2227 // Collection properties should be read only
#pragma warning restore CA1515 // Consider making public types internal
#pragma warning restore IDE0079 // Remove unnecessary suppression
