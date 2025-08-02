namespace BuzzAir.Utilities;

internal class InMemoryCachingService(IMemoryCache memoryCache) : ICachingService
{
    public Task SetAsync<T>(string key, T @object, CancellationToken token)
    {
        _ = memoryCache.Set(key, @object);
        return Task.CompletedTask;
    }

    public async Task<T> GetAsync<T>(string key, Func<CancellationToken, Task<T>> getObject, CancellationToken token = default)
    {
        if (memoryCache.TryGetValue(key, out T? @object) && @object != null)
        {
            return @object;
        }

        @object = await getObject(token);
        await SetAsync(key, @object, token);

        return @object;
    }

    public Task RemoveAsync(string key, CancellationToken token = default)
    {
        memoryCache.Remove(key);
        return Task.CompletedTask;
    }
}
