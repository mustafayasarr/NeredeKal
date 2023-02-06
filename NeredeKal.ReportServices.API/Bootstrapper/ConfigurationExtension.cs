using Microsoft.EntityFrameworkCore;
using NeredeKal.ReportServices.Infrastructure.Context;
using NeredeKal.ReportServices.Infrastructure.Repositories.Abstract;
using NeredeKal.ReportServices.Infrastructure.Repositories.Concrete;
using MediatR;

namespace NeredeKal.ReportServices.API.Bootstrapper
{
	public static class ConfigurationExtension
	{
		public static IServiceCollection RegisterConfigurationServices(this IServiceCollection services, IConfiguration Configuration)
		{
			services.AddDbContext<ReportDbContext>(options =>
					options.UseNpgsql(Configuration.GetConnectionString("DevelopmentDbConnection")));

			#region Lifetime

			services.AddScoped<IReportItemRepository, ReportItemRepository>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			#endregion


			services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
		
			return services;
		}
	}
}
