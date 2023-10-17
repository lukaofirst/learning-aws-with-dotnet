using Domain.Entities;

namespace Domain.Interfaces;

public interface IConsumerService
{
	Task<string> GetQueueUrlAsync();
	Task<List<DomainMessage>> StartAsync(string queueUrl);
}