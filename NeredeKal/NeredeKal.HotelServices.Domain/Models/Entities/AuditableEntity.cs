namespace NeredeKal.HotelServices.Domain.Models.Entities
{
	public abstract class AuditableEntity
	{
		public DateTime CreatedAtUTC { get; set; }
		public DateTime? ModifiedAtUTC { get; set; }
		public bool IsDeleted { get; set; }
	}
}
