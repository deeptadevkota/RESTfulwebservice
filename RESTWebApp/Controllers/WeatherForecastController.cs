using Azure.Storage.Queues;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace RESTWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            Console.WriteLine("GET method called = I");
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                AppID = "1",
                AppName = "DEMO-testing"
            })
            .ToArray();
        }

       

        [HttpPost]
        public async Task Post()
        {
            Console.WriteLine($"POST method called {HttpContext.Request.Form["AppID"]}, {HttpContext.Request.Form["AppName"]}");
            WeatherForecast weather = new WeatherForecast()
            {
                AppID = HttpContext.Request.Form["AppID"], AppName = HttpContext.Request.Form["AppName"]
            };
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=sparklistenerpoc;AccountKey=mjBlWQWWLayGf/UghbddpYZUDnemnqjSJ+ypbIHYSYhi+uYjZrXSsihe97iEC9G/NlgVzia8Tarr+ASt+3aACA==;EndpointSuffix=core.windows.net";
            var queueName = "callback-data";
            var queueClient = new QueueClient(connectionString, queueName);
            var message = JsonSerializer.Serialize(weather);
            await queueClient.SendMessageAsync(message);

        }


    }
}