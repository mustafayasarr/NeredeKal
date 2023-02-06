using FluentValidation;
using FluentValidation.AspNetCore;
using NeredeKal.ReportServices.Domain.Filters;
using NeredeKal.ReportServices.Domain.Models.Messaging.Commands;

namespace NeredeKal.ReportServices.API.Bootstrapper
{
	public static class FluentValidationExtension
	{
		public static IServiceCollection RegisterFluentValidation(this IServiceCollection services)
		{
			services.AddControllersWithViews(option =>
			{
				option.Filters.Add(new ValidationFilter());
			}).AddFluentValidation(conf =>
			{
				conf.RegisterValidatorsFromAssembly(typeof(Program).Assembly);
				conf.AutomaticValidationEnabled = false;
			});

			var types = typeof(CreateLocationReportCommand).Assembly.GetTypes();
			new AssemblyScanner(types).ForEach(pair =>
			{
				services.Add(ServiceDescriptor.Transient(pair.InterfaceType, pair.ValidatorType));
			});

			return services;
		}
	}
}
