using System;
using System.Net.Http;

namespace AZCEAzureFunction._01_CallGetWeatherForecastFunction
{
    public class ResponseDto
    {
        public WeatherForecastDto[] Uri3RdPartyResults { get; set; }
        public string Base3RdPartyCalled { get; set; }
        public string Uri3RdPartyCalled { get; set; }
        public HttpMethod Uri3RdPartyMethod { get; set; }
    }

    public class WeatherForecastDto
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }

    public enum MethodType
    {
        Get,
        Post
    }
}
