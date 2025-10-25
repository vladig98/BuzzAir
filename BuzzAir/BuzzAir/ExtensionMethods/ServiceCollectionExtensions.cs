namespace BuzzAir.ExtensionMethods;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
    {
        string cs = config.GetConnectionString("DefaultConnection")
                 ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        _ = services.AddDbContext<BuzzAirDbContext>(o =>
            o.UseNpgsql(cs, x => x.MigrationsAssembly(typeof(BuzzAirDbContext).GetTypeInfo().Assembly.GetName().Name)).EnableSensitiveDataLogging());

        _ = services.AddSingleton<IDataSeeder, DataSeeder>();

        return services;
    }

    public static IServiceCollection AddIdentityServices(this IServiceCollection services)
    {
        _ = services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<BuzzAirDbContext>()
                .AddDefaultTokenProviders()
                .AddDefaultUI();

        _ = services.Configure<IdentityOptions>(opts =>
            {
                opts.Password.RequireDigit = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequiredLength = 3;
                opts.Password.RequiredUniqueChars = 0;

                opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(9999999);
                opts.Lockout.MaxFailedAccessAttempts = 999999;
                opts.Lockout.AllowedForNewUsers = false;

                opts.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                opts.User.RequireUniqueEmail = false;
            });

        return services;
    }

    public static IServiceCollection AddCaching(this IServiceCollection services, IConfiguration config)
    {
        _ = services.AddSingleton<IMemoryCache, MemoryCache>();
        _ = services.AddSingleton(sp =>
            {
                IMemoryCache memory = sp.GetRequiredService<IMemoryCache>();
                using IServiceScope scope = sp.CreateScope();

                using BuzzAirDbContext db = scope.ServiceProvider.GetRequiredService<BuzzAirDbContext>();
                string? redisCs = config.GetConnectionString("Redis");

                return CacheFactory.GetCachingService(redisCs, memory, db);
            });

        return services;
    }

    public static IServiceCollection AddAuthenticationProviders(this IServiceCollection services, IConfiguration config)
    {
        _ = services.AddAuthentication()
                .AddFacebook(opts =>
                {
                    opts.AppId = config["Authentication:Facebook:AppId"] ?? string.Empty;
                    opts.AppSecret = config["Authentication:Facebook:AppSecret"] ?? string.Empty;
                    opts.AccessDeniedPath = "/AccessDeniedPathInfo";
                });

        return services;
    }

    public static IServiceCollection AddCookiePolicy(this IServiceCollection services)
    {
        _ = services.Configure<CookiePolicyOptions>(opts =>
            {
                opts.CheckConsentNeeded = ctx => true;
                opts.MinimumSameSitePolicy = SameSiteMode.None;
                opts.ConsentCookieValue = "true";
            });

        _ = services.ConfigureApplicationCookie(opts =>
            {
                opts.Events.OnRedirectToLogin = ctx =>
                {
                    ctx.Response.Headers.Location = ctx.RedirectUri;
                    ctx.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
                opts.Events.OnRedirectToAccessDenied = ctx =>
                {
                    ctx.Response.StatusCode = 403;
                    return Task.CompletedTask;
                };
            });

        return services;
    }

    public static IServiceCollection AddMvcOptions(this IServiceCollection services)
    {
        _ = services.Configure<MvcViewOptions>(opts =>
            opts.HtmlHelperOptions.CheckBoxHiddenInputRenderMode = CheckBoxHiddenInputRenderMode.None);

        _ = services.Configure<RazorViewEngineOptions>(opts =>
            opts.AreaViewLocationFormats.Add("~/Views/Shared/{0}.cshtml"));

        return services;
    }
}
