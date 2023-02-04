using Microsoft.EntityFrameworkCore.Storage;
using NeredeKal.HotelServices.Infrastructure.Context;
using NeredeKal.HotelServices.Infrastructure.Repositories.Abstract;

namespace NeredeKal.HotelServices.Infrastructure.Repositories.Concrete
{
	public class UnitOfWork : IUnitOfWork
	{
		IDbContextTransaction transaction;
		ApplicationDbContext _context;
		public UnitOfWork(ApplicationDbContext context)
		{
			_context = context;
			ContactRepository = new ContactRepository(_context);
			HotelRepository = new HotelRepository(_context);


			transaction = context.Database.BeginTransaction();
		}
		public IContactRepository ContactRepository { get; }
		public IHotelRepository HotelRepository { get; }
		public void Complete(bool state = true)
		{
			_context.SaveChanges();
			if (state)
			{
				transaction.Commit();
			}
			else
			{
				transaction.Rollback();
			}
			Dispose();
		}

		public async Task CompleteAsync(bool state = true)
		{
			await _context.SaveChangesAsync();
			if (state)
			{
				transaction.Commit();
			}
			else
			{
				transaction.Rollback();
			}
			Dispose();
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
