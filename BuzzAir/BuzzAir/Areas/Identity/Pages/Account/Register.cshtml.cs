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
    BuzzAirDbContext context) : PageModel
{
    [BindProperty]
    public RegisterInputModel Input { get; set; } = null!;
    public string ReturnUrl { get; set; } = string.Empty;
    public IList<AuthenticationScheme> ExternalLogins { get; set; } = [];
    public IEnumerable<SelectListItem> Countries { get; set; } = [];

    public async Task OnGetAsync(string? returnUrl = null)
    {
        ReturnUrl = returnUrl ?? string.Empty;
        ExternalLogins = [.. await signInManager.GetExternalAuthenticationSchemesAsync()];
    }

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        returnUrl ??= Url.Content("~/Identity/Account/Confirm");
        if (ModelState.IsValid)
        {
            IdentityRole role = new()
            {
                Name = !context.AppUsers.Any() ? "Admin" : "User"
            };

            bool x = await roleManager.RoleExistsAsync(role.Name);
            if (!x)
            {
                _ = await roleManager.CreateAsync(role);
            }

            City city = GetCity(Input.City);

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
            _ = await userManager.AddToRoleAsync(user, role.Name);
            _ = await userManager.AddClaimAsync(user, claim: new Claim(ClaimTypes.Role.ToString(), role.Name));
            if (result.Succeeded)
            {
                logger.LogInformation("User created a new account with password.");
                //this.context.UserRoles.Add(new IdentityUserRole<string> { RoleId = role.Id, UserId = user.Id });
                //this.context.SaveChanges();

                string code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                string? callbackUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { userId = user.Id, code },
                    protocol: Request.Scheme);

                await emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl ?? string.Empty)}'>clicking here</a>.");

                //await _signInManager.SignInAsync(user, isPersistent: false);
                return LocalRedirect(returnUrl);
            }
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        // If we got this far, something failed, redisplay form
        return Page();
    }

    private City GetCity(string city)
    {
        throw new NotImplementedException();
    }
}
#pragma warning restore CA1056 // URI-like properties should not be strings
#pragma warning restore CA1054 // URI-like parameters should not be strings
#pragma warning restore CA2227 // Collection properties should be read only
#pragma warning restore IDE0079 // Remove unnecessary suppression
