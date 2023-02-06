using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeredeKal.ReportServices.Domain.Models.Entities
{
	public abstract class EntityBase<TId> : AuditableEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public TId Id { get; set; }
	}
}
