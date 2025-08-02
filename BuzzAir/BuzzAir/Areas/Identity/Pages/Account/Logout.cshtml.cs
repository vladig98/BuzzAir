// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
namespace BuzzAir.Areas.Identity.Pages.Account;

#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable CA1056 // URI-like properties should not be strings
#pragma warning disable CA1515 // Consider making public types internal
#pragma warning disable CA1054 // URI-like parameters should not be strings
public sealed class LogoutModel(SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger) : PageModel
{
    public async Task<IActionResult> OnPost(string? returnUrl = null)
    {
        await signInManager.SignOutAsync();
        logger.LogInformation("User logged out.");
        if (returnUrl != null)
        {
            return LocalRedirect(returnUrl);
        }
        else
        {
            // This needs to be a redirect so that the browser performs a new
            // request and the identity for the user gets updated.
            return RedirectToPage();
        }
    }
}
#pragma warning restore CA1056 // URI-like properties should not be strings
#pragma warning restore CA1054 // URI-like parameters should not be strings
#pragma warning restore CA1515 // Consider making public types internal
#pragma warning restore IDE0079 // Remove unnecessary suppression
