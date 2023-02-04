using Microsoft.EntityFrameworkCore;
using NeredeKal.HotelServices.Infrastructure.Context;
using NeredeKal.HotelServices.Infrastructure.Repositories.Abstract;
using NeredeKal.HotelServices.Infrastructure.Repositories.Concrete;

namespace NeredeKal.HotelServices.API.Bootstrapper
{
	public static class ConfigurationExtension
	{
		public static IServiceCollection RegisterConfigurationServices(this IServiceCollection services, IConfiguration Configuration)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
					options.UseNpgsql(Configuration.GetConnectionString("DevelopmentDbConnection")));

			#region Lifetime
			services.AddScoped<IContactRepository, ContactRepository>();
			services.AddScoped<IHotelRepository, HotelRepository>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			#endregion


			return services;
		}
	}
}
