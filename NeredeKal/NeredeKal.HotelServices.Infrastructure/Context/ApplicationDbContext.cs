using Microsoft.EntityFrameworkCore;
using NeredeKal.HotelServices.Domain.Models.Entities;

namespace NeredeKal.HotelServices.Infrastructure.Context
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}
		public DbSet<Hotel> Hotel { get; set; }
		public DbSet<Contact> Contact { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Hotel>().HasQueryFilter(b => !b.IsDeleted);
			modelBuilder.Entity<Contact>().HasQueryFilter(b => !b.IsDeleted);
		}
		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
		{
			foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
			{
				switch (entry.State)
				{
					case EntityState.Added:
						{
							if (entry.Entity.CreatedAtUTC == DateTime.MinValue)
							{
								entry.Entity.CreatedAtUTC = DateTime.UtcNow;
								entry.Entity.IsDeleted = false;
							}
							break;
						}

					case EntityState.Modified:
						{
							if (entry.Entity.ModifiedAtUTC == null)
							{
								entry.Entity.ModifiedAtUTC = DateTime.UtcNow;
							}
							break;
						}
					case EntityState.Deleted:
						{
							entry.State = EntityState.Modified;
							entry.Entity.IsDeleted = true;
							if (entry.Entity.ModifiedAtUTC == null)
							{
								entry.Entity.ModifiedAtUTC = DateTime.UtcNow;
							}
							break;
						}
				}
			}
			return base.SaveChangesAsync(cancellationToken);

		}
	}
}
