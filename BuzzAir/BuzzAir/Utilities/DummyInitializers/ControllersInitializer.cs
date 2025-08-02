namespace BuzzAir.Utilities.DummyInitializers;

internal static class ControllersInitializer
{
    public static void EnsureInitialized()
    {
        using HomeController homeController = new();
    }
}
