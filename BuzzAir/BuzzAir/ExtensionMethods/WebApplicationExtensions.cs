namespace BuzzAir.ExtensionMethods;

internal static class WebApplicationExtensions
{
    public static WebApplication ConfigureErrorHandling(this WebApplication app)
    {
        _ = !app.Environment.IsDevelopment()
            ? app.UseExceptionHandler("/Home/Error")
                 .UseHsts()
                 .UseWebSockets()
            : app.UseDeveloperExceptionPage();
        _ = app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");

        return app;
    }

    public static WebApplication ConfigureStaticFiles(this WebApplication app)
    {
        _ = app.UseHttpsRedirection()
               .UseStaticFiles()
               .UseCookiePolicy();

        return app;
    }

    public static WebApplication ConfigureRoutingAndAuth(this WebApplication app)
    {
        _ = app.UseRouting()
               .UseAuthentication()
               .UseAuthorization();

        return app;
    }

    public static WebApplication ConfigureEndpoints(this WebApplication app)
    {
        _ = app.MapRazorPages();

        _ = app.MapControllerRoute(
            name: "IdentityArea",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

        _ = app.MapAreaControllerRoute(
            name: "AdminArea",
            areaName: "Admin",
            pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

        _ = app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        return app;
    }

    public static WebApplication MapSignalRHubs(this WebApplication app)
    {
        _ = app.MapHub<LocationHub>("/locationHub");
        _ = app.MapHub<FlightHub>("/flightHub");

        return app;
    }
}
