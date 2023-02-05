using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;

namespace NeredeKal.HotelServices.API.Bootstrapper
{
	public static class ElasticConfigurationExtension
	{
		public static IServiceCollection RegisterElasticConfiguration(this IServiceCollection services, WebApplicationBuilder builder)
		{
			var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
			Log.Logger = new LoggerConfiguration()
				.Enrich.FromLogContext()
				.Enrich.WithMachineName()
			.WriteTo.Debug()
			.WriteTo.Console()
				.WriteTo.Elasticsearch(ConfigureElasticSink(builder.Configuration, environment))
				.Enrich.WithProperty("Environment", environment)
				.ReadFrom.Configuration(builder.Configuration)
				.CreateLogger();


			return services;
		}
		private static ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
		{
			return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
			{
				AutoRegisterTemplate = true,
				IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
			};
		}
	}
}
