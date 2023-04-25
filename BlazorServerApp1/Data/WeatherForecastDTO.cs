namespace BlazorServerApp1.Data
{
    public class ResponseDto
    {
        public WeatherForecastDTO[] Uri3RdPartyResults { get; set; }
        public string BaseUrl { get; set; }
        public string FunctionUri { get; set; }
        
        public HttpMethod FunctionMethod { get; set; }

        public string Base3RdPartyCalled { get; set; }
        public string Uri3RdPartyCalled { get; set; }
        public HttpMethod Uri3RdPartyMethod { get; set; }
    }

    public class WeatherForecastDTO
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }
}