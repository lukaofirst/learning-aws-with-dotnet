using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using Domain.Options;
using Microsoft.Extensions.Options;

namespace Infra.Services
{
	public abstract class BaseSQSService
	{
		private readonly IOptions<AWSServices> _options;
		private readonly IAmazonSQS _amazonSQS;

		public BaseSQSService(IAmazonSQS amazonSQS, IOptions<AWSServices> options)
		{
			_amazonSQS = amazonSQS;
			_options = options;
		}

		private protected async Task<GetQueueUrlResponse> GetQueueUrlResponse()
		{
			var awsServices = _options.Value;

			var getQueueUrlRequest = new GetQueueUrlRequest(awsServices.SQSQueueName);

			var queueUrlResponse = await _amazonSQS.GetQueueUrlAsync(getQueueUrlRequest);

			return queueUrlResponse;
		}
	}
}