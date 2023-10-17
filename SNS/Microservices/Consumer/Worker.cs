using Domain.Interfaces;
namespace Consumer;

public class Worker : BackgroundService
{
	private readonly ILogger<Worker> _logger;
	private readonly IConsumerService _consumerService;

	public Worker(ILogger<Worker> logger, IConsumerService consumerService)
	{
		_logger = logger;
		_consumerService = consumerService;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		var queueUrl = await _consumerService.GetQueueUrlAsync();
		while (!stoppingToken.IsCancellationRequested)
		{
			_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

			var result = await _consumerService.StartAsync(queueUrl);

			if (result is null)
				continue;

			result.ForEach(x => Console.WriteLine(x.ToString()));

			//await Task.Delay(1000, stoppingToken);
		}
	}
}
