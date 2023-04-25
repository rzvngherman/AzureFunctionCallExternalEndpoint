using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace BlazorServerApp1.Data.WeatherForecastServiceExternal
{
    public interface IWeatherForecastServiceExternal
    {
        Task<ResponseDto> GetForecastAsync(DateOnly startDate);
    }

    public class WeatherForecastServiceExternal : IWeatherForecastServiceExternal
    {
        // http://localhost:7207/api/CallExternalWeatherForecastFunction
        private const string BaseUrl = "http://localhost:7207";
        private const string functionUri = "/api/CallExternalWeatherForecastFunction";
        private HttpMethod functionMethod = HttpMethod.Get;

        public async Task<ResponseDto> GetForecastAsync(DateOnly startDate)
        {
            HttpClient _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(BaseUrl);

            //GET
            HttpRequestMessage msg = new HttpRequestMessage(functionMethod, functionUri);

            var response = await _httpClient.SendAsync(msg);
            if (!response.IsSuccessStatusCode)
            {
                StringBuilder message = new StringBuilder();
                message.Append($"SendAsync for uri {functionUri}, encountered {response.StatusCode} ");
                var responseErrorBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                message.Append(responseErrorBody);
                throw new InvalidOperationException(message.ToString());
            }
            else
            {
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                //T checkResponse = JsonConvert.DeserializeObject<T>(responseBody);
                var checkResponse = JsonConvert.DeserializeObject<ResponseDto>(responseBody);
                checkResponse.BaseUrl = BaseUrl;
                checkResponse.FunctionUri = functionUri;
                checkResponse.FunctionMethod = functionMethod;
                return checkResponse;
            }
        }
    }
}
