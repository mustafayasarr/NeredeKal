using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace NeredeKal.ReportServices.Infrastructure.Repositories.Abstract
{
	public interface IRepository<T> where T : class
	{
		DbSet<T> Table { get; }

		void Add(T entity);
		void AddRange(IEnumerable<T> entity);
		void Update(T entity);

		Task<int> CreateAsync(T entity, CancellationToken cancellationToken = default);

		Task<int> UpdateAsync(T entity, CancellationToken cancellationToken = default);

		Task<int> UpdateRangeAsync(IEnumerable<T> entity);

		void UpdateRange(IEnumerable<T> entity);

		void Delete(T entity);

		void DeleteRange(IEnumerable<T> entities);

		Task<int> CreateRangeAsync(IEnumerable<T> entity, CancellationToken cancellationToken = default);

		Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> where, CancellationToken cancellationToken = default);
		Task<T> FirstOrDefaultAsync(CancellationToken cancellationToken = default);

		Task<List<T>> ToListAsync(Expression<Func<T, bool>> where, CancellationToken cancellationToken = default);
		Task<List<T>> ToListAsync(CancellationToken cancellationToken = default);
		Task<bool> ExistsAsync(Expression<Func<T, bool>> where, CancellationToken cancellationToken = default);

		IQueryable<T> All();

		IQueryable<T> Where(Expression<Func<T, bool>> where);

		IQueryable<T> OrderBy<TKey>(Expression<Func<T, TKey>> orderBy, bool isDesc);
		Task<int> SaveAsync(CancellationToken cancellationToken = default);
	}
}
