using NeredeKal.HotelServices.Domain.Models.Entities;
using NeredeKal.HotelServices.Infrastructure.Context;
using NeredeKal.HotelServices.Infrastructure.Repositories.Abstract;

namespace NeredeKal.HotelServices.Infrastructure.Repositories.Concrete
{
	public class HotelRepository : Repository<Hotel>, IHotelRepository
	{
		public HotelRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
		}
	}
}
