using Microsoft.EntityFrameworkCore;
using NeredeKal.ReportServices.Infrastructure.Context;
using NeredeKal.ReportServices.Infrastructure.Repositories.Abstract;
using System.Linq.Expressions;

namespace NeredeKal.ReportServices.Infrastructure.Repositories.Concrete
{
	public abstract class Repository<T> : IRepository<T> where T : class
	{
		private readonly ReportDbContext _dbContext;

		public Repository(ReportDbContext dbContext)
		{
			_dbContext = dbContext;
			Table = dbContext.Set<T>();
		}
		public DbSet<T> Table { get; set; }

		public void Add(T entity)
		{
			Table.Add(entity);
		}

		public void AddRange(IEnumerable<T> entity)
		{
			Table.AddRange(entity);
		}

		public IQueryable<T> All()
		{
			return Table;
		}

		public async Task<int> CreateAsync(T entity, CancellationToken cancellationToken = default)
		{
			await Table.AddAsync(entity, cancellationToken);

			return await SaveAsync(cancellationToken);
		}

		public async Task<int> CreateRangeAsync(IEnumerable<T> entity, CancellationToken cancellationToken = default)
		{
			await Table.AddRangeAsync(entity, cancellationToken);

			return await SaveAsync(cancellationToken);
		}

		public async void Delete(T entity)
		{
			Table.Remove(entity);
		}

		public async void DeleteRange(IEnumerable<T> entities)
		{
			Table.RemoveRange(entities);
		}

		public async Task<bool> ExistsAsync(Expression<Func<T, bool>> where, CancellationToken cancellationToken = default)
		{
			return await Table.AnyAsync(where, cancellationToken);
		}

		public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> where, CancellationToken cancellationToken = default)
		{


			return await Table.FirstOrDefaultAsync(where, cancellationToken);
		}

		public async Task<T> FirstOrDefaultAsync(CancellationToken cancellationToken = default)
		{
			return await Table.FirstOrDefaultAsync(cancellationToken);
		}

		public IQueryable<T> OrderBy<TKey>(Expression<Func<T, TKey>> orderBy, bool isDesc)
		{

			if (isDesc)
			{
				return Table.OrderByDescending(orderBy);
			}

			return Table.OrderBy(orderBy);
		}

		public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
		{
			try
			{
				return await _dbContext.SaveChangesAsync(cancellationToken);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<List<T>> ToListAsync(Expression<Func<T, bool>> where, CancellationToken cancellationToken = default)
		{
			return await Table.Where(where).ToListAsync(cancellationToken);
		}

		public async Task<List<T>> ToListAsync(CancellationToken cancellationToken = default)
		{
			return await Table.ToListAsync(cancellationToken);
		}

		public void Update(T entity)
		{
			Table.Update(entity);
		}

		public async Task<int> UpdateAsync(T entity, CancellationToken cancellationToken = default)
		{
			Table.Update(entity);

			return await SaveAsync(cancellationToken);
		}

		public void UpdateRange(IEnumerable<T> entity)
		{
			Table.UpdateRange(entity);
		}

		public async Task<int> UpdateRangeAsync(IEnumerable<T> entity)
		{
			Table.UpdateRange(entity);

			return await SaveAsync();
		}

		public IQueryable<T> Where(Expression<Func<T, bool>> where)
		{
			return Table.Where(where);
		}
	}
}
