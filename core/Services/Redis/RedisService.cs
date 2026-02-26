using System.Text.Json;
using StackExchange.Redis;

namespace Core.Services.Redis
{
    public class RedisService : IRedisService
    {
        private readonly IDatabase _database;
        private readonly IConnectionMultiplexer _redis;

        public RedisService(IConnectionMultiplexer redis)
        {
            _redis = redis;
            _database = _redis.GetDatabase();
        }

        public Task SetAccessTokenAsync(Guid userId, string token, TimeSpan expiry) =>
            _database.StringSetAsync($"auth_token:{userId}", token, expiry);

        public async Task<string> GetAccessTokenAsync(Guid userId)
        {
            var val = await _database.StringGetAsync($"auth_token:{userId}");
            return val.HasValue ? val.ToString() : null;
        }

        public Task RemoveAccessTokenAsync(Guid userId) =>
            _database.KeyDeleteAsync($"auth_token:{userId}");

        public Task SetRefreshTokenAsync(Guid userId, string refreshToken, TimeSpan expiry) =>
            _database.StringSetAsync($"refresh_token:{userId}", refreshToken, expiry);

        public async Task<string> GetRefreshTokenAsync(Guid userId)
        {
            var val = await _database.StringGetAsync($"refresh_token:{userId}");
            return val.HasValue ? val.ToString() : null;
        }

        public Task RemoveRefreshTokenAsync(Guid userId) =>
            _database.KeyDeleteAsync($"refresh_token:{userId}");

        public Task BlacklistJtiAsync(string jti, TimeSpan expiry) =>
            _database.StringSetAsync($"blacklist_jti:{jti}", "revoked", expiry);

        public async Task<bool> IsJtiBlacklistedAsync(string jti)
        {
            var result = await _database.StringGetAsync($"blacklist_jti:{jti}");
            return result.HasValue;
        }

        public async Task SetStringAsync(string key, string value, TimeSpan? expiry = null)
        {
            await _database.StringSetAsync(key, value, expiry);
        }

        public async Task<string> GetStringAsync(string key)
        {
            var value = await _database.StringGetAsync(key);
            return value.HasValue ? value.ToString() : null;
        }

        public async Task RemoveAsync(string key)
        {
            await _database.KeyDeleteAsync(key);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            var json = JsonSerializer.Serialize(value);
            await _database.StringSetAsync(key, json, expiry);
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var value = await _database.StringGetAsync(key);
            return value.HasValue ? JsonSerializer.Deserialize<T>(value!) : default;
        }

        public async Task<bool> KeyExistsAsync(string key)
        {
            return await _database.KeyExistsAsync(key);
        }
    }
}
