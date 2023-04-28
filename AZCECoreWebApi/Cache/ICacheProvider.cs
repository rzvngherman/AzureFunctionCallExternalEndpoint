using AZCECoreWebApi.Db;
using Microsoft.Extensions.Caching.Memory;
using System.Threading;

namespace AZCECoreWebApi.Cache
{
    public interface ICacheProvider
    {
        Task<IEnumerable<WeatherForecast>> GetCachedWeatherForecast();
    }

    public class CacheProvider : ICacheProvider
    {
        private static readonly SemaphoreSlim GetUsersSemaphore = new SemaphoreSlim(1, 1);
        private readonly IMemoryCache _cache;
        private readonly AppDbContext _context;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public CacheProvider(IMemoryCache memoryCache, AppDbContext context)
        {
            _cache = memoryCache;
            _context = context;
        }

        public async Task<IEnumerable<WeatherForecast>> GetCachedWeatherForecast()
        {
            try
            {
                return await GetCachedResponse(CacheKeys.WeatherForecast, GetUsersSemaphore);
            }
            catch
            {
                throw;
            }
        }

        private async Task<IEnumerable<WeatherForecast>> GetCachedResponse(
            string cacheKey,
            SemaphoreSlim semaphore)
        {
            bool isAvaiable = _cache.TryGetValue(cacheKey, out List<WeatherForecast> employees);
            if (isAvaiable) return employees;
            try
            {
                await semaphore.WaitAsync();
                isAvaiable = _cache.TryGetValue(cacheKey, out employees);
                if (isAvaiable) return employees;

                employees = _context.WeatherForecast.ToList();
                var arr = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                .ToArray();
#if DEBUG
                if (employees.Count == 0)
                {
                    _context.WeatherForecast.AddRange(arr);
                    await _context.SaveChangesAsync();
                }
#endif
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                    SlidingExpiration = TimeSpan.FromMinutes(2),
                    Size = 1024,
                };
                _cache.Set(cacheKey, employees, cacheEntryOptions);
            }
            catch
            {
                throw;
            }
            finally
            {
                semaphore.Release();
            }
            return employees;
        }
    }
}
