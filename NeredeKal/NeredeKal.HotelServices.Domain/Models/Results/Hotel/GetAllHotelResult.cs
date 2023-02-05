using NeredeKal.HotelServices.Domain.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeredeKal.HotelServices.Domain.Models.Results.Hotel
{
	public class GetAllHotelResult
	{
		public GetAllHotelResult(string id, string authorizedName, string authorizedLastName, string companyName, DateTime createdDate, List<ContactDto> contacts = null)
		{
			Id = Guid.Parse(id);
			AuthorizedName = authorizedName;
			AuthorizedLastName = authorizedLastName;
			CompanyName = companyName;
			CreatedDate = createdDate;
			Contacts = contacts;
		}
		public GetAllHotelResult()
		{
			Contacts = new List<ContactDto>();
		}
		public Guid Id { get; set; }
		public string AuthorizedName { get; set; }
		public string AuthorizedLastName { get; set; }
		public string CompanyName { get; set; }
		public DateTime CreatedDate { get; set; }
		public List<ContactDto> Contacts { get; set; }
	}
}
