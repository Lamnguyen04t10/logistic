namespace Core.Abstractions
{
    public interface IRedisCacheService
    {
        Task<string?> GetAsync(string key);
        Task SetAsync(string key, string value, TimeSpan? expiry = null);
    }
}
