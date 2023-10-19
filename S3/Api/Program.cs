using Amazon.S3;
using Api.Interfaces;
using Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var awsSettings = builder.Configuration.GetAWSOptions();
builder.Services.AddDefaultAWSOptions(awsSettings);
builder.Services.AddAWSService<IAmazonS3>();

builder.Services.AddScoped<IS3Service, S3Service>();

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
