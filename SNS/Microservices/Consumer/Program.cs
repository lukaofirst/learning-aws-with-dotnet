using Consumer;
using IoC;

IHost host = Host.CreateDefaultBuilder(args)
	.ConfigureServices((services) =>
	{
		var serviceProvider = services.BuildServiceProvider();
		var configuration = serviceProvider.GetRequiredService<IConfiguration>();

		services.AddAppServices(configuration);
		services.AddHostedService<Worker>();
	})
	.Build();

host.Run();
