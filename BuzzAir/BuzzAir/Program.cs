WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSignalR();

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddIdentityServices();
builder.Services.AddCaching(builder.Configuration);
builder.Services.AddAuthenticationProviders(builder.Configuration);
builder.Services.AddCookiePolicy();
builder.Services.AddMvcOptions();
builder.Services.AddCustomAppServices();

WebApplication app = builder.Build();

app.ConfigureErrorHandling();
app.ConfigureStaticFiles();
app.ConfigureRoutingAndAuth();
app.ConfigureEndpoints();
app.MapSignalRHubs();

app.Run();