WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
        throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<BuzzAirDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<BuzzAirDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

builder.Services.AddSingleton<IMemoryCache, MemoryCache>();
builder.Services.AddSingleton(sp =>
{
    ICachingService cachingService;
    ILogger logger = sp.GetRequiredService<ILogger<Program>>();

    try
    {
        string redisConnectionString = builder.Configuration.GetConnectionString("Redis") ??
            throw new InvalidOperationException("Connection string 'Redis' not found.");

        ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
        IDatabase redis = connectionMultiplexer.GetDatabase();

        cachingService = new RedisCachingService(redis);
    }
    catch (Exception)
    {
        IMemoryCache memoryCache = sp.GetRequiredService<IMemoryCache>();
        cachingService = new InMemoryCachingService(memoryCache);
    }

    using IServiceScope scope = sp.CreateScope();
    using BuzzAirDbContext dbContext = scope.ServiceProvider.GetRequiredService<BuzzAirDbContext>();

    City[] cities = [.. dbContext.Cities.Include(x => x.State).Include(x => x.Country).AsNoTracking()];
    State[] states = [.. dbContext.States.Include(x => x.Country).AsNoTracking()];
    Country[] countries = [.. dbContext.Countries.AsNoTracking()];
    Aircraft[] aircraft = [.. dbContext.Aircrafts.AsNoTracking()];
    Airport[] airports = [.. dbContext.Airports.Include(x => x.City).ThenInclude(x => x.State).Include(x => x.City).ThenInclude(x => x.Country).AsNoTracking()];

    foreach (City city in cities)
    {
        string key = string.Format(CultureInfo.InvariantCulture, GlobalConstants.CITY_CACHE_KEY, city.Id);
        _ = cachingService.SetAsync(key, city, CancellationToken.None);
    }

    foreach (State state in states)
    {
        string key = string.Format(CultureInfo.InvariantCulture, GlobalConstants.STATE_CACHE_KEY, state.Id);
        _ = cachingService.SetAsync(key, state, CancellationToken.None);
    }

    foreach (Country country in countries)
    {
        string key = string.Format(CultureInfo.InvariantCulture, GlobalConstants.COUNTRY_CACHE_KEY, country.Id);
        _ = cachingService.SetAsync(key, country, CancellationToken.None);
    }

    foreach (Aircraft air in aircraft)
    {
        string key = string.Format(CultureInfo.InvariantCulture, GlobalConstants.AIRCRAFT_CACHE_KEY, air.Id);
        _ = cachingService.SetAsync(key, air, CancellationToken.None);
    }

    foreach (Airport airport in airports)
    {
        string key = string.Format(CultureInfo.InvariantCulture, GlobalConstants.AIRPORT_CACHE_KEY, airport.Id);
        _ = cachingService.SetAsync(key, airport, CancellationToken.None);
    }

    _ = cachingService.SetAsync(GlobalConstants.CITIES_CACHE_KEY, cities.Where(x => !x.IsDeleted), CancellationToken.None);
    _ = cachingService.SetAsync(GlobalConstants.CITIES_DELETED_CACHE_KEY, cities.Where(x => x.IsDeleted), CancellationToken.None);

    _ = cachingService.SetAsync(GlobalConstants.COUNTRIES_CACHE_KEY, countries.Where(x => !x.IsDeleted), CancellationToken.None);
    _ = cachingService.SetAsync(GlobalConstants.COUNTRIES_DELETED_CACHE_KEY, countries.Where(x => x.IsDeleted), CancellationToken.None);

    _ = cachingService.SetAsync(GlobalConstants.STATES_CACHE_KEY, states.Where(x => !x.IsDeleted), CancellationToken.None);
    _ = cachingService.SetAsync(GlobalConstants.STATES_DELETED_CACHE_KEY, states.Where(x => x.IsDeleted), CancellationToken.None);

    _ = cachingService.SetAsync(GlobalConstants.AIRCRAFT_ALL_CACHE_KEY, aircraft.Where(x => !x.IsDeleted), CancellationToken.None);
    _ = cachingService.SetAsync(GlobalConstants.AIRCRAFT_DELETED_ALL_CACHE_KEY, aircraft.Where(x => x.IsDeleted), CancellationToken.None);

    _ = cachingService.SetAsync(GlobalConstants.AIRPORTS_CACHE_KEY, airports.Where(x => !x.IsDeleted), CancellationToken.None);
    _ = cachingService.SetAsync(GlobalConstants.AIRPORTS_DELETED_CACHE_KEY, airports.Where(x => x.IsDeleted), CancellationToken.None);

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
    options.HtmlHelperOptions.CheckBoxHiddenInputRenderMode = CheckBoxHiddenInputRenderMode.None);

ConfigurationManager configuration = builder.Configuration;
builder.Services.AddAuthentication().AddFacebook(facebookOptions =>
{
    facebookOptions.AppId = configuration["Authentication:Facebook:AppId"] ?? string.Empty;
    facebookOptions.AppSecret = configuration["Authentication:Facebook:AppSecret"] ?? string.Empty;
    facebookOptions.AccessDeniedPath = "/AccessDeniedPathInfo";
});

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

builder.Services.Configure<RazorViewEngineOptions>(options => options.AreaViewLocationFormats.Add("~/Views/Shared/{0}.cshtml"));

WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    _ = app.UseExceptionHandler("/Home/Error");
    _ = app.UseHsts();
    _ = app.UseWebSockets();
}

app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");
app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

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