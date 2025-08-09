namespace BuzzAir.Areas.Identity.Pages.Account;

#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable CA1056 // URI-like properties should not be strings
#pragma warning disable CA2227 // Collection properties should be read only
#pragma warning disable CA1054 // URI-like parameters should not be strings
public sealed class RegisterModel(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    ILogger<RegisterModel> logger,
    IEmailSender emailSender,
    RoleManager<IdentityRole> roleManager,
    ICountryService countryService,
    ICityService cityService) : PageModel
{
    [BindProperty]
    public RegisterInputModel Input { get; set; } = null!;
    public string ReturnUrl { get; set; } = string.Empty;
    public IList<AuthenticationScheme> ExternalLogins { get; set; } = [];

    public IEnumerable<SelectListItem> Countries { get; set; } = [];
    public IEnumerable<SelectListItem> States { get; set; } = [];
    public IEnumerable<SelectListItem> Cities { get; set; } = [];

    public async Task OnGetAsync(string? returnUrl = null)
    {
        ReturnUrl = returnUrl ?? string.Empty;
        ExternalLogins = [.. await signInManager.GetExternalAuthenticationSchemesAsync()];

        List<CountryDTO> countries = await countryService.GetAllCountriesAsync(null, null);

        SelectListGroup countryGroup = new() { Name = "Officially recognized countries" };
        SelectListGroup dependencyGroup = new() { Name = "Territories not officially recognized as coutnries" };

        Countries = countries.Select(c => new SelectListItem()
        {
            Text = c.Name,
            Value = c.Id,
            Group = c.IsOfficiallyRecognizedCountry ? countryGroup : dependencyGroup
        });
    }

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");
        ExternalLogins = [.. await signInManager.GetExternalAuthenticationSchemesAsync()];

        if (!ModelState.IsValid)
        {
            return Page();
        }

        const string roleName = "User";

        IdentityRole role = new(roleName);
        role.Name ??= roleName;

        if (!await roleManager.RoleExistsAsync(role.Name))
        {
            _ = await roleManager.CreateAsync(role);
        }

        City city = await cityService.GetCityModelByIdAsync(Input.CityId);

        ApplicationUser user = new()
        {
            Id = Guid.NewGuid().ToString(),
            Email = Input.Email,
            PhoneNumber = Input.PhoneNumber,
            UserName = Input.Username,
            FirstName = Input.FullName,
            LastName = Input.FullName,
            Gender = Input.Gender,
            PostalCode = Input.Postal,
            Street = Input.Street,
            City = city,
            CityId = city.Id,
        };

        IdentityResult result = await userManager.CreateAsync(user, Input.Password);

        if (result.Succeeded)
        {
            _ = await userManager.AddToRoleAsync(user, role.Name);
            _ = await userManager.AddClaimAsync(user, claim: new Claim(ClaimTypes.Role.ToString(), role.Name));

            logger.LogInformation("User created a new account with password.");

            string code = await userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            string? callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = user.Id, code },
                protocol: Request.Scheme);

            await emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl ?? string.Empty)}'>clicking here</a>.");

            if (userManager.Options.SignIn.RequireConfirmedAccount)
            {
                return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl });
            }
            else
            {
                await signInManager.SignInAsync(user, isPersistent: false);
                return LocalRedirect(returnUrl);
            }
        }

        foreach (IdentityError error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        // If we got this far, something failed, redisplay form
        return Page();
    }
}
#pragma warning restore CA1056 // URI-like properties should not be strings
#pragma warning restore CA1054 // URI-like parameters should not be strings
#pragma warning restore CA2227 // Collection properties should be read only
#pragma warning restore IDE0079 // Remove unnecessary suppression