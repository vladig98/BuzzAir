// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
namespace BuzzAir.Areas.Identity.Pages.Account.Manage;

#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable CA1515 // Consider making public types internal
public sealed class PersonalDataModel(UserManager<ApplicationUser> userManager) : PageModel
#pragma warning restore CA1515 // Consider making public types internal
#pragma warning restore IDE0079 // Remove unnecessary suppression
{
    public async Task<IActionResult> OnGet()
    {
        ApplicationUser? user = await userManager.GetUserAsync(User);
        return user == null ? NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.") : Page();
    }
}
