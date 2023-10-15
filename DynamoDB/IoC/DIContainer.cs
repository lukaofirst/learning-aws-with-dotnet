using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Application.Interfaces;
using Application.Mappings;
using Application.Services;
using Domain.Interfaces;
using Infra;
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
		services.AddAWSService<IAmazonDynamoDB>();
		services.AddScoped<IDynamoDBContext, DynamoDBContext>();

		// Application
		services.AddAutoMapper(typeof(AutoMapperMappings).Assembly);
		services.AddScoped<IPersonService, PersonService>();

		// Infra
		services.AddScoped<IPersonRepository, PersonRepository>();
	}
}