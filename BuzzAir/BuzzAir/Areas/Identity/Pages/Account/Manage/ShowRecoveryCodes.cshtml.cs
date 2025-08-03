// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
namespace BuzzAir.Areas.Identity.Pages.Account.Manage;

/// <summary>
///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
///     directly from your code. This API may change or be removed in future releases.
/// </summary>
#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable CA1056 // URI-like properties should not be strings
#pragma warning disable CA2227 // Collection properties should be read only
#pragma warning disable CA1819 // Properties should not return arrays
#pragma warning disable CA1054 // URI-like parameters should not be strings
public sealed class ShowRecoveryCodesModel : PageModel
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

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public IActionResult OnGet()
    {
        return RecoveryCodes == null || RecoveryCodes.Length == 0 ? RedirectToPage("./TwoFactorAuthentication") : Page();
    }
}
#pragma warning restore CA1056 // URI-like properties should not be strings
#pragma warning restore CA1054 // URI-like parameters should not be strings
#pragma warning restore CA1819 // Properties should not return arrays
#pragma warning restore CA1819 // Properties should not return arrays
#pragma warning restore IDE0079 // Remove unnecessary suppression
