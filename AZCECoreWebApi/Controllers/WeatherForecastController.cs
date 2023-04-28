using AZCECoreWebApi.Cache;
using Microsoft.AspNetCore.Mvc;

namespace AZCECoreWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ICacheProvider _cacheProvider;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ICacheProvider cacheProvider)
        {
            _logger = logger;
            _cacheProvider = cacheProvider;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var employees = await _cacheProvider.GetCachedWeatherForecast();
            return employees;
        }
    }
}