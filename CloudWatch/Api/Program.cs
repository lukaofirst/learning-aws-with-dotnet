using AWS.Logger;
using AWS.Logger.SeriLog;
using Serilog;
using Serilog.Formatting.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

/* ----- Using Serilog ----- */
// builder.Host.UseSerilog((ctx, lg) =>
// {
// 	var awsLoggerConfigSection = builder.Configuration.GetAWSLoggingConfigSection();
// 	var logGroup = awsLoggerConfigSection.Config.LogGroup;
// 	var region = awsLoggerConfigSection.Config.Region;

// 	var awsLoggerConfig = new AWSLoggerConfig(logGroup)
// 	{
// 		Region = region
// 	};

// 	lg
// 		.WriteTo.AWSSeriLog(awsLoggerConfig, textFormatter: new JsonFormatter())
// 		.WriteTo.Console(new JsonFormatter());
// });

/* ----- Using ILogger ----- */
builder.Services.AddLogging(config =>
{
	config.AddAWSProvider(builder.Configuration.GetAWSLoggingConfigSection());
	config.AddConsole();
	config.SetMinimumLevel(LogLevel.Debug);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
