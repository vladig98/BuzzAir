WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
        throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<BuzzAirDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<BuzzAirDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect("localhost"));

builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IConnectionMultiplexer>().GetDatabase());

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
    options.ConsentCookieValue = "true";
});

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;
    options.Password.RequiredUniqueChars = 0;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(9999999);
    options.Lockout.MaxFailedAccessAttempts = 999999;
    options.Lockout.AllowedForNewUsers = false;

    // User settings.
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

builder.Services.Configure<MvcViewOptions>(options =>
{
    // Disable hidden checkboxes
    options.HtmlHelperOptions.CheckBoxHiddenInputRenderMode = CheckBoxHiddenInputRenderMode.None;
});

IConfiguration configuration = builder.Configuration;

builder.Services.AddAuthentication().AddFacebook(facebookOptions =>
{
    facebookOptions.AppId = configuration["Authentication:Facebook:AppId"] ?? string.Empty;
    facebookOptions.AppSecret = configuration["Authentication:Facebook:AppSecret"] ?? string.Empty;
    facebookOptions.AccessDeniedPath = "/AccessDeniedPathInfo";
});

builder.Services.AddTransient<IAirportService, AirportService>();
builder.Services.AddTransient<IFlightsService, FlightsService>();
builder.Services.AddTransient<IAircraftService, AircraftService>();
builder.Services.AddTransient<IBookingService, BookingService>();
builder.Services.AddTransient<IUserBookingService, UserBookingService>();
builder.Services.AddTransient<IPaymentService, PaymentService>();
builder.Services.AddTransient<IBookingFlightService, BookingFlightService>();
builder.Services.AddTransient<IBookingPassengerService, BookingPassengerService>();
builder.Services.AddTransient<ITimezoneService, TimezoneService>();
builder.Services.AddTransient<ICountryService, CountryService>();
builder.Services.AddTransient<ICityService, CityService>();
builder.Services.AddTransient<IStateService, StateService>();
builder.Services.AddTransient<IPassengerService, PassengerService>();
builder.Services.AddTransient<IPassengerServiceService, PassengerServiceService>();
builder.Services.AddTransient<IFlightPassengerService, FlightPassengerService>();
builder.Services.AddTransient<IServiceService, ServiceService>();
builder.Services.AddTransient<IBoardingPassService, BoardingPassService>();
builder.Services.AddTransient<IPriceCalculator, PriceCalculator>();
builder.Services.AddTransient<ISeatService, SeatService>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.Headers.Location = context.RedirectUri;
        context.Response.StatusCode = 401;

        return Task.CompletedTask;
    };
    options.Events.OnRedirectToAccessDenied = context =>
    {
        context.Response.StatusCode = 403;

        return Task.CompletedTask;
    };
});

builder.Services.AddRazorPages();
builder.Services.AddSignalR();

builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    options.AreaViewLocationFormats.Add("~/Views/Shared/{0}.cshtml");
});

WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");

app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseDataSeeder();

app.MapRazorPages();
app.MapHub<SelectHub>("/getSelectOptions");

app.MapControllerRoute(
    name: "IdentityArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();