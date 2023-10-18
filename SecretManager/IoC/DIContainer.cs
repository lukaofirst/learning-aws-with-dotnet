using System.Text.Json;
using Amazon.SecretsManager;
using Application.Interfaces;
using Application.Mappings;
using Application.Services;
using Domain.Interfaces;
using Infra;
using Infra.AWS.Interfaces;
using Infra.AWS.Models;
using Infra.AWS.Services;
using Infra.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IoC;

public static class DIContainer
{
	public const string AWS_SECRETNAME = "Example-SecretManager";

	public static void AddAppServices(this IServiceCollection services, IConfiguration configuration)
	{
		// AWS
		var awsSettings = configuration.GetAWSOptions();
		services.AddDefaultAWSOptions(awsSettings);
		services.AddAWSService<IAmazonSecretsManager>();
		services.AddScoped<ISecretManagerService, SecretManagerService>();

		// Application
		services.AddScoped<IPersonService, PersonService>();
		services.AddAutoMapper(typeof(AutoMapperMappings).Assembly);

		// Infra
		services.AddScoped<IPersonRepository, PersonRepository>();

		string? sqlServerConnectionString = string.Empty;

		using (var serviceScope = services.BuildServiceProvider().CreateScope())
		{
			var secretManagerService = serviceScope.ServiceProvider.GetRequiredService<ISecretManagerService>();
			var secretStringResult = secretManagerService
				.GetSecretAsync(AWS_SECRETNAME, awsSettings.Region.SystemName).Result;

			var secretStringResultJson = JsonSerializer.Deserialize<EFCoreSecretManagerDTO>(
				secretStringResult,
				new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
			)!;

			sqlServerConnectionString = $@"
				Server={secretStringResultJson.Host},{secretStringResultJson.Port};
				Database={secretStringResultJson.Database};
				User Id={secretStringResultJson.Username};
				Password={secretStringResultJson.Password};
				TrustServerCertificate=true
			";
		};


		services.AddDbContext<DataContext>(opts => opts.UseSqlServer(sqlServerConnectionString));
	}
}