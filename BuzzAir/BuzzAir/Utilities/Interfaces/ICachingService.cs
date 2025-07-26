namespace BuzzAir.Utilities.Interfaces
{
    public interface ICachingService
    {
        /// <summary>
        /// Add an item to the cache.
        /// </summary>
        Task SetAsync<T>(string key, T @object, CancellationToken token);

        /// <summary>
        /// Get an item from the cache or default to a function, e.g. pull from DB.
        /// </summary>
        Task<T> GetAsync<T>(string key, Func<CancellationToken, Task<T>> getObject, CancellationToken token = default);

        /// <summary>
        /// Remove an item from the cache.
        /// </summary>
        Task RemoveAsync(string key, CancellationToken token = default);
    }
}
