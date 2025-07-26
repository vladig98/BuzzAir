namespace BuzzAir.Utilities
{
    public class RedisCachingService(IDatabase redis) : ICachingService
    {
        private readonly JsonSerializerSettings _jsonOptions = new()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore,
            TypeNameHandling = TypeNameHandling.All
        };

        public async Task<T> GetAsync<T>(string key, Func<CancellationToken, Task<T>> getObject, CancellationToken token = default)
        {
            T? @object = default;
            string? objectAsJSON = await redis.StringGetAsync(key);

            if (!string.IsNullOrEmpty(objectAsJSON))
            {
                @object = JsonConvert.DeserializeObject<T?>(objectAsJSON, _jsonOptions);
            }

            if (@object != null)
            {
                return @object;
            }

            @object = await getObject(token);
            await SetAsync(key, @object, token);

            return @object;
        }

        public Task SetAsync<T>(string key, T @object, CancellationToken token)
        {
            string objectAsJSON = JsonConvert.SerializeObject(@object, _jsonOptions);
            return redis.StringSetAsync(key, objectAsJSON);
        }

        public Task RemoveAsync(string key, CancellationToken token = default) =>
            redis.StringGetDeleteAsync(key);
    }
}
