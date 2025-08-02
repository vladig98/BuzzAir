// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
namespace BuzzAir.Areas.Identity.Pages.Account.Manage;

internal sealed class PersonalDataModel(UserManager<ApplicationUser> userManager) : PageModel
{
    public async Task<IActionResult> OnGet()
    {
        ApplicationUser? user = await userManager.GetUserAsync(User);
        return user == null ? NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.") : Page();
    }
}
