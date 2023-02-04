using NeredeKal.HotelServices.Domain.Models.Entities;
using NeredeKal.HotelServices.Infrastructure.Context;
using NeredeKal.HotelServices.Infrastructure.Repositories.Abstract;

namespace NeredeKal.HotelServices.Infrastructure.Repositories.Concrete
{
	public class ContactRepository : Repository<Contact>, IContactRepository
	{
		public ContactRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
		}
	}
}
