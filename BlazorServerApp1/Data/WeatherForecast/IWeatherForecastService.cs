namespace BlazorServerApp1.Data.WeatherForecast
{
    public interface IWeatherForecastService
    {
        Task<WeatherForecastDTO[]> GetForecastAsync(DateTime startDate);
    }
}