using Live_File_Organizer.Model;

namespace Live_File_Organizer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int delay = 1000;
            while (!stoppingToken.IsCancellationRequested)
            {
                Organizer organizer = new();
                organizer.StartOrganizer();
                delay = organizer.GetMillisecondsFromMinutes();
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(delay, stoppingToken);
            }
        }
    }
}