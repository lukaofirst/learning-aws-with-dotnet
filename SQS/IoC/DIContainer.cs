using Amazon.SQS;
using Domain.Interfaces;
using Domain.Options;
using Infra.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IoC;

public static class DIContainer
{
	public static void AddAppServices(this IServiceCollection services, IConfiguration configuration)
	{
		// AWS
		var awsSettings = configuration.GetAWSOptions();
		services.AddDefaultAWSOptions(awsSettings);
		services.AddAWSService<IAmazonSQS>();

		// Infra
		services.AddSingleton<IConsumerService, ConsumerService>();
		services.AddScoped<IPublishService, PublishService>();

		// Binding Options
		var c = configuration.GetSection(nameof(AWSServices));
		services.Configure<AWSServices>(c);
	}
}