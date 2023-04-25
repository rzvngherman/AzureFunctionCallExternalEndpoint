using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Text;
using AZCEAzureFunction._01_CallGetWeatherForecastFunction;

namespace AZCEAzureFunction
{
    public static class CallExternalWeatherForecastFunction
    {
        private const string uri = "/WeatherForecast";

        [FunctionName("CallExternalWeatherForecastFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                HttpClient newClient = new HttpClient();
                newClient.BaseAddress = new Uri("https://localhost:7150");

                HttpRequestMessage newRequest = new HttpRequestMessage(HttpMethod.Get, uri);

                //Read Server Response
                HttpResponseMessage response = await newClient.SendAsync(newRequest);
                if (!response.IsSuccessStatusCode)
                {
                    StringBuilder message = new StringBuilder();
                    message.Append($"SendAsync for uri {uri}, encountered {response.StatusCode} ");
                    var responseErrorBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    message.Append(responseErrorBody);
                    throw new InvalidOperationException(message.ToString());
                }
                else
                {
                    var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var checkResponse = JsonConvert.DeserializeObject<WeatherForecastDto[]>(responseBody);
                    var result = new ResponseDto
                    {
                        Uri3RdPartyResults = checkResponse,
                        MethodType = HttpMethod.Get.ToString(),
                        Uri3RdPartyCalled = uri
                    };

                    return new OkObjectResult(result);
                }
            }
            catch (Exception ex)
            {
                return new OkObjectResult(ex.ToString());
            }
        }

        //[FunctionName("HttpExample")]
        //public static async Task<IActionResult> Run(
        //    [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
        //    [Queue("outqueue"), StorageAccount("AzureWebJobsStorage")] ICollector<string> msg,
        //    ILogger log)
        //{
        //    log.LogInformation("C# HTTP trigger function processed a request.");

        //    string name = req.Query["name"];

        //    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        //    dynamic data = JsonConvert.DeserializeObject(requestBody);
        //    name = name ?? data?.name;

        //    if (!string.IsNullOrEmpty(name))
        //    {
        //        // Add a message to the output collection.
        //        msg.Add(string.Format("Name passed to the function: {0}", name));
        //    }

        //    string responseMessage = string.IsNullOrEmpty(name)
        //        ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
        //        : $"Hello, {name}. This HTTP triggered function executed successfully.";

        //    return new OkObjectResult(responseMessage);
        //}
    }
}
