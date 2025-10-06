using BuzzAir.Services.DataSeeders.Interfaces;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddValidators();
builder.Services.AddSeeders();

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddIdentityServices();
builder.Services.AddCaching(builder.Configuration);
builder.Services.AddAuthenticationProviders(builder.Configuration);
builder.Services.AddCookiePolicy();
builder.Services.AddMvcOptions();
builder.Services.AddCustomAppServices();

WebApplication app = builder.Build();

IDataSeeder dataSeeder = app.Services.GetRequiredService<IDataSeeder>();
await dataSeeder.SeedAsync();

app.ConfigureErrorHandling();
app.ConfigureStaticFiles();
app.ConfigureRoutingAndAuth();
app.ConfigureEndpoints();
app.MapSignalRHubs();

await app.RunAsync();