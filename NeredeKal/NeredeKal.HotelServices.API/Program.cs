using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NeredeKal.HotelServices.API.Bootstrapper;
using NeredeKal.HotelServices.Infrastructure.Context;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLogging();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.RegisterConfigurationServices(builder.Configuration);
builder.Services.RegisterFluentValidation();
builder.Services.RegisterElasticConfiguration(builder);
builder.Services.AddCap(options =>
{
	options.UseEntityFramework<ApplicationDbContext>();
	options.UsePostgreSql(builder.Configuration.GetConnectionString("DevelopmentDbConnection"));
	options.UseRabbitMQ(a =>
	{
		a.HostName = builder.Configuration.GetSection("RabbitMQHost:HostName").Value;
		a.UserName = builder.Configuration.GetSection("RabbitMQHost:UserName").Value;
		a.Password = builder.Configuration.GetSection("RabbitMQHost:Password").Value;
	});
	options.UseDashboard();

});

builder.Host.UseSerilog();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
	var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
	db.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

