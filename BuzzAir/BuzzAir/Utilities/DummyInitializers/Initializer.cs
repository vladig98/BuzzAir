namespace BuzzAir.Utilities.DummyInitializers;

internal static class Initializer
{
    public static void EnsureInitialized()
    {
        _ = Task.Run(() =>
        {
            try
            {
                IdentityPageModelsInitializer.EnsureInitialized();
                IdentityPageAccountModelsInitializer.EnsureInitialized();
                IdentityPageInputModelsInitializer.EnsureInitialized();
                ControllersInitializer.EnsureInitialized();
            }
            catch { }
        });
    }
}
