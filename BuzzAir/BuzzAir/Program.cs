using BuzzAir.Data;
using BuzzAir.Hubs;
using BuzzAir.Middleware;
using BuzzAir.Models.DbModels;
using BuzzAir.Services;
using BuzzAir.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
        throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<BuzzAirDbContext>(options => options.UseSqlServer(connectionString, opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<BuzzAirDbContext>().AddDefaultTokenProviders().AddDefaultUI();

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
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

builder.Services.Configure<MvcViewOptions>(options =>
{
    // Disable hidden checkboxes
    options.HtmlHelperOptions.CheckBoxHiddenInputRenderMode = CheckBoxHiddenInputRenderMode.None;
});

var configuration = builder.Configuration;

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

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.Headers["Location"] = context.RedirectUri;
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

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");

app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();
app.UseStaticFiles();

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

public partial class Program { }
