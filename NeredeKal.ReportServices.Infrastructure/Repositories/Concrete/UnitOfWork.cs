using Microsoft.EntityFrameworkCore.Storage;
using NeredeKal.ReportServices.Infrastructure.Context;
using NeredeKal.ReportServices.Infrastructure.Repositories.Abstract;

namespace NeredeKal.ReportServices.Infrastructure.Repositories.Concrete
{
	public class UnitOfWork : IUnitOfWork
	{
		IDbContextTransaction transaction = null;
		ReportDbContext _context;
		public UnitOfWork(ReportDbContext context)
		{
			_context = context;
			ReportItemRepository = new ReportItemRepository(_context);


			transaction = context.Database.BeginTransaction();
		}
		public IReportItemRepository ReportItemRepository { get; }

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
