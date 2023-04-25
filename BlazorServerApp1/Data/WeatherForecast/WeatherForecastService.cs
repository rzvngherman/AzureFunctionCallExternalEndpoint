namespace BlazorServerApp1.Data.WeatherForecast
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public Task<WeatherForecastDTO[]> GetForecastAsync(DateTime startDate)
        {
            var results = Enumerable.Range(1, 5).Select(index => new WeatherForecastDTO
            {
                Date = startDate.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToArray();

            return Task.FromResult(results);
        }
    }
}