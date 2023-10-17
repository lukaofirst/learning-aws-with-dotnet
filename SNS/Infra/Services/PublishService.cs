using System.Net;
using System.Text.Json;
using Amazon.SQS;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Options;
using Microsoft.Extensions.Options;

namespace Infra.Services;

public class PublishService : BaseSQSService, IPublishService
{
	private readonly IAmazonSQS _amazonSQS;

	public PublishService(IAmazonSQS amazonSQS, IOptions<AWSServices> options)
		: base(amazonSQS, options)
	{
		_amazonSQS = amazonSQS;
	}

	public async Task<bool> SendAsync(DomainMessage message)
	{
		var queueUrlResponse = await GetQueueUrlResponse();
		var jsonMessage = JsonSerializer.Serialize(message);

		var sendMessageResponse = await _amazonSQS.SendMessageAsync(queueUrlResponse.QueueUrl, jsonMessage);

		var result = sendMessageResponse.HttpStatusCode.Equals(HttpStatusCode.OK);

		return result;
	}
}