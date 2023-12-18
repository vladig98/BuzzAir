namespace BuzzAir.Middleware
{
    public static class DataSeederExtensions
    {
        public static IApplicationBuilder UseDataSeeder(this IApplicationBuilder app)
        {
            return app.UseMiddleware<DataSeeder>();
        }
    }
}
