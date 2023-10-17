using System.Text.Json;
using Amazon.SQS;
using Amazon.SQS.Model;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Options;
using Microsoft.Extensions.Options;

namespace Infra.Services;

public class ConsumerService : BaseSQSService, IConsumerService
{
	private readonly IAmazonSQS _amazonSQS;

	public ConsumerService(IAmazonSQS amazonSQS, IOptions<AWSServices> options)
		: base(amazonSQS, options)
	{
		_amazonSQS = amazonSQS;
	}

	public async Task<string> GetQueueUrlAsync()
	{
		var getQueueUrlResponse = await GetQueueUrlResponse();

		return getQueueUrlResponse.QueueUrl;
	}

	public async Task<List<DomainMessage>> StartAsync(string queueUrl)
	{
		var receiveMessageRequest = new ReceiveMessageRequest
		{
			MaxNumberOfMessages = 1,
			//VisibilityTimeout = 30,
			QueueUrl = queueUrl,
			WaitTimeSeconds = 1,
			/*
				If WaitTimeSeconds equals to 0 => short polling
				If WaitTimeSeconds greater than 0 => long polling
			*/
		};

		var receiveMessageResponse = await _amazonSQS.ReceiveMessageAsync(receiveMessageRequest);

		if (!receiveMessageResponse.Messages.Any())
			return Enumerable.Empty<DomainMessage>().ToList();

		var result = new List<DomainMessage>();

		foreach (var message in receiveMessageResponse.Messages)
		{
			var messageParsed = JsonSerializer.Deserialize<DomainMessage>(message.Body);

			result.Add(messageParsed!);

			var deleteMessageRequest = new DeleteMessageRequest
			{
				QueueUrl = queueUrl,
				ReceiptHandle = message.ReceiptHandle,
			};

			await _amazonSQS.DeleteMessageAsync(deleteMessageRequest);
		}

		return result;
	}
}