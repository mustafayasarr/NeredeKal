using FluentValidation;
using FluentValidation.AspNetCore;
using NeredeKal.HotelServices.Domain.Filters;
using NeredeKal.HotelServices.Domain.Models.Messaging.Commands.Contact;

namespace NeredeKal.HotelServices.API.Bootstrapper
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

			var types = typeof(CreateContactValidator).Assembly.GetTypes();
			new AssemblyScanner(types).ForEach(pair =>
			{
				services.Add(ServiceDescriptor.Transient(pair.InterfaceType, pair.ValidatorType));
			});

			return services;
		}
	}
}
