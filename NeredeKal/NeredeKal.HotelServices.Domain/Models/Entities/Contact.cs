using NeredeKal.HotelServices.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
