using Domain.Interfaces;
using Domain.Options;
using Microsoft.Extensions.Options;

namespace Consumer;

public class Worker : BackgroundService
{
	private readonly ILogger<Worker> _logger;
	private readonly IConsumerService _consumerService;
	private readonly IOptions<AWSServices> _options;

	public Worker(ILogger<Worker> logger, IConsumerService consumerService, IOptions<AWSServices> options)
	{
		_logger = logger;
		_consumerService = consumerService;
		_options = options;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		var queueUrl = await _consumerService.GetQueueUrlAsync();
		while (!stoppingToken.IsCancellationRequested)
		{
			_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

			var result = await _consumerService.StartAsync(queueUrl);

			if (result is null)
				return;

			result.ForEach(x => Console.WriteLine(x.ToString()));

			await Task.Delay(1000, stoppingToken);
		}
	}
}
