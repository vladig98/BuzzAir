WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
        throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<BuzzAirDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<BuzzAirDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

builder.Services.AddSingleton<IMemoryCache, MemoryCache>();
builder.Services.AddSingleton(sp =>
{
    ICachingService cachingService;
    ILogger logger = sp.GetRequiredService<ILogger<Program>>();
    logger.LogError("Getting cache service");

    try
    {
        string redisConnectionString = builder.Configuration.GetConnectionString("Redis") ??
            throw new InvalidOperationException("Connection string 'Redis' not found.");

        logger.LogError("Trying to connect to redis");

        ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
        IDatabase redis = connectionMultiplexer.GetDatabase();

        cachingService = new RedisCachingService(redis);
    }
    catch (Exception)
    {
        logger.LogError("Failed connecting to redis.");

        IMemoryCache memoryCache = sp.GetRequiredService<IMemoryCache>();
        cachingService = new InMemoryCachingService(memoryCache);
    }

    //BuzzAirDbContext dbContext = sp.GetRequiredService<BuzzAirDbContext>();
    //City[] cities = [.. dbContext.Cities.Include(x => x.State).Include(x => x.Country).AsNoTracking()];
    //State[] states = [.. dbContext.States.Include(x => x.Country).AsNoTracking()];
    //Country[] countries = [.. dbContext.Countries.AsNoTracking()];

    //foreach (City city in cities)
    //{
    //    cachingService.SetAsync(string.Format(GlobalConstants.CITY_CACHE_KEY, city.Id), city, CancellationToken.None);
    //}

    //foreach (State state in states)
    //{
    //    cachingService.SetAsync(string.Format(GlobalConstants.STATE_CACHE_KEY, state.Id), state, CancellationToken.None);
    //}

    //foreach (Country country in countries)
    //{
    //    cachingService.SetAsync(string.Format(GlobalConstants.COUNTRY_CACHE_KEY, country.Id), country, CancellationToken.None);
    //}

    return cachingService;
});

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

// Repositories
builder.Services.AddScoped<IAircraftRepository, AircraftRepository>();
builder.Services.AddScoped<IAirportRepository, AirportRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IStateRepository, StateRepository>();

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
    app.UseWebSockets();
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

app.MapAreaControllerRoute(
  name: "AdminArea",
  areaName: "Admin",
  pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();