namespace Core.Services.Redis
{
    public interface IRedisService
    {
        Task SetAccessTokenAsync(Guid userId, string token, TimeSpan expiry);
        Task<string> GetAccessTokenAsync(Guid userId);
        Task RemoveAccessTokenAsync(Guid userId);
        Task SetRefreshTokenAsync(Guid userId, string refreshToken, TimeSpan expiry);
        Task<string> GetRefreshTokenAsync(Guid userId);
        Task RemoveRefreshTokenAsync(Guid userId);

        Task SetStringAsync(string key, string value, TimeSpan? expiry = null);
        Task<string> GetStringAsync(string key);
        Task RemoveAsync(string key);
        Task SetAsync<T>(string key, T value, TimeSpan? expiry = null);
        Task<T> GetAsync<T>(string key);
        Task<bool> KeyExistsAsync(string key);

        Task BlacklistJtiAsync(string jti, TimeSpan expiry);
        Task<bool> IsJtiBlacklistedAsync(string jti);
    }
}
