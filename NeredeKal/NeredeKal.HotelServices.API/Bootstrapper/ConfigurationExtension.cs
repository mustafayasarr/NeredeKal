using Microsoft.EntityFrameworkCore;
using NeredeKal.HotelServices.Infrastructure.Context;
using NeredeKal.HotelServices.Infrastructure.Repositories.Abstract;
using NeredeKal.HotelServices.Infrastructure.Repositories.Concrete;
using MediatR;
using NeredeKal.HotelServices.Core.Gateways.ReportService;
using NeredeKal.HotelServices.Core.Gateways;
using NeredeKal.HotelServices.Core.Subscribers.Report;

namespace NeredeKal.HotelServices.API.Bootstrapper
{
	public static class ConfigurationExtension
	{
		public static IServiceCollection RegisterConfigurationServices(this IServiceCollection services, IConfiguration Configuration)
		{
			services.AddSwaggerGen();

			services.AddDbContext<ApplicationDbContext>(options =>
					options.UseNpgsql(Configuration.GetConnectionString("DevelopmentDbConnection")));

			#region Lifetime
			services.AddScoped<IContactRepository, ContactRepository>();
			services.AddScoped<IHotelRepository, HotelRepository>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddSingleton<IRestService, RestService>();
			services.AddSingleton<IReportGateway, ReportGateway>();
			services.AddTransient<CreateReportSubscriber>();

			#endregion

			services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
			
			return services;
		}

	}

}
