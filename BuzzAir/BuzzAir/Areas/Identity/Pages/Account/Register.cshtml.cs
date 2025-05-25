using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace BuzzAir.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;
        private readonly BuzzAirDbContext context;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ICountryService countryService,
            RoleManager<IdentityRole> roleManager,
            BuzzAirDbContext context,
            ICityService cityService)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _countryService = countryService;
            _roleManager = roleManager;
            this.context = context;
            _cityService = cityService;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "FullName")]
            public string FullName { get; set; }

            [Required]
            [Display(Name = "Username")]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.PhoneNumber)]
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }

            [Required]
            [Display(Name = "Street")]
            public string Street { get; set; }

            [Required]
            [Display(Name = "City")]
            public string City { get; set; }

            [Required]
            [Display(Name = "Country")]
            public string Country { get; set; }

            [Required]
            [Display(Name = "Postal Code")]
            public string Postal { get; set; }

            [Required]
            [Display(Name = "Gender")]
            public Gender Gender { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            List<SelectListItem> countries = new List<SelectListItem>();

            SelectListGroup countryGroup = new SelectListGroup { Name = "Countries" };
            SelectListGroup dependenciesGroup = new SelectListGroup { Name = "Dependencies and territories not offically recognized as countries" };

            var countryValues = await _countryService.GetAllAsync();

            foreach (Country country in countryValues)
            {
                countries.Add(new SelectListItem()
                {
                    Text = country.Name,
                    Value = country.Id,
                    Group = country.IsCountry ? countryGroup : dependenciesGroup
                });
            }

            Countries = countries.OrderBy(x => x.Group.Name).ThenBy(x => x.Text);

            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/Identity/Account/Confirm");
            if (ModelState.IsValid)
            {
                var role = new IdentityRole();
                if (!this.context.AppUsers.Any())
                {
                    role.Name = "Admin";
                }
                else
                {
                    role.Name = "User";
                }
                bool x = await _roleManager.RoleExistsAsync(role.Name);
                if (!x)
                {
                    await _roleManager.CreateAsync(role);
                }

                var country = await _countryService.GetByIdAsync(Input.Country);
                var city = await _cityService.GetByNameAsync(Input.City);

                if (city == null)
                {
                    city = await _cityService.CreateAsync(country, Input.City);
                }

                var address = new Address { Id = Guid.NewGuid().ToString(), City = city, Country = country, PostalCode = Input.Postal, Street = Input.Street };
                var user = new ApplicationUser { Id = Guid.NewGuid().ToString(), Email = Input.Email, PhoneNumber = Input.PhoneNumber, UserName = Input.Username, FirstName = Input.FullName, LastName = Input.FullName, Address = address, Gender = Input.Gender, Role = role };
                var result = await _userManager.CreateAsync(user, Input.Password);
                await _userManager.AddToRoleAsync(user, role.Name);
                await _userManager.AddClaimAsync(user, claim: new Claim(ClaimTypes.Role.ToString(), role.Name));
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    //this.context.UserRoles.Add(new IdentityUserRole<string> { RoleId = role.Id, UserId = user.Id });
                    //this.context.SaveChanges();

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}
