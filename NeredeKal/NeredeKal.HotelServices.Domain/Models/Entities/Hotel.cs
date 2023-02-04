using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeredeKal.HotelServices.Domain.Models.Entities
{
	public class Hotel:EntityBase<Guid>
	{
		public string AuthorizedName { get; set; }
		public string AuthorizedLastName { get; set; }
		public string CompanyName { get; set; }
		public ICollection<Contact> Contacts { get; set; }
	}
}
