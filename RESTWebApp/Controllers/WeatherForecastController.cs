using Microsoft.AspNetCore.Mvc;

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
            Console.WriteLine("GET method called");
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                AppID = 1,
                AppName = "DEMO-testing"
            })
            .ToArray();
        }

        [HttpPost(Name = "GetWeatherForecast")]
        public void Post(WeatherForecast weather)
        {
            Console.WriteLine($"POST method called {weather.AppID}, {weather.AppName}");

            return;
        }

    }
}