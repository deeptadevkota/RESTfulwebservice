using Azure.Storage.Queues;
using System.Text.Json;

namespace RESTWebApp
{
    public class WeatherDataService : BackgroundService
    {
        private readonly ILogger<WeatherDataService> _logger;

        public WeatherDataService(ILogger<WeatherDataService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=sparklistenerpoc;AccountKey=mjBlWQWWLayGf/UghbddpYZUDnemnqjSJ+ypbIHYSYhi+uYjZrXSsihe97iEC9G/NlgVzia8Tarr+ASt+3aACA==;EndpointSuffix=core.windows.net";
            var queueName = "callback-data";
            var queueClient = new QueueClient(connectionString, queueName);
             
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Reading from queue");
                var queueMessage = await queueClient.ReceiveMessageAsync();

                if(queueMessage.Value != null)
                {
                    var weatherData = JsonSerializer.Deserialize<WeatherForecast>(queueMessage.Value.MessageText);
                    _logger.LogInformation("New message read: {weatherData}", weatherData);

                    await queueClient.DeleteMessageAsync(queueMessage.Value.MessageId, queueMessage.Value.PopReceipt);
                }

                await Task.Delay(TimeSpan.FromSeconds(10));

            }
        }
    }
}
