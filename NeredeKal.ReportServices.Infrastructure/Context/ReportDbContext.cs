using Microsoft.EntityFrameworkCore;
using NeredeKal.ReportServices.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeredeKal.ReportServices.Infrastructure.Context
{
	public class ReportDbContext : DbContext
	{
		public ReportDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
		{

		}
		public DbSet<ReportItem> ReportItem { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ReportItem>().HasQueryFilter(b => !b.IsDeleted);
			base.OnModelCreating(modelBuilder);
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
