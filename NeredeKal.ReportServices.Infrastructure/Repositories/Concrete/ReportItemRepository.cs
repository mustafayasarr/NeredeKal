using NeredeKal.ReportServices.Domain.Models.Entities;
using NeredeKal.ReportServices.Infrastructure.Context;
using NeredeKal.ReportServices.Infrastructure.Repositories.Abstract;

namespace NeredeKal.ReportServices.Infrastructure.Repositories.Concrete
{
	public class ReportItemRepository : Repository<ReportItem>, IReportItemRepository
	{
		public ReportItemRepository(ReportDbContext dbContext) : base(dbContext)
		{
		}
	}
}
