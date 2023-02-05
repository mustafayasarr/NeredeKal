using NeredeKal.HotelServices.Domain.Models.Enums;

namespace NeredeKal.HotelServices.Domain.Models.Entities
{
	public class Contact:EntityBase<Guid>
	{
		public Contact()
		{

		}
		public Contact(ContactType contactType, string contactContent, Guid hotelId)
		{
			ContactType = contactType;
			Content = contactContent;
			HotelId = hotelId;
		}
		public ContactType ContactType { get; set; }
		public string Content { get; set; }
		public Guid HotelId { get; set; }
		public Hotel Hotel { get; set; }
	}
}
