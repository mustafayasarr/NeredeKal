using NeredeKal.HotelServices.Domain.Models.Dtos;

namespace NeredeKal.HotelServices.Domain.Models.Results.Hotel
{
	public sealed class GetHotelResult
	{
		public GetHotelResult(string id, string authorizedName, string authorizedLastName, string companyName, DateTime createdDate, List<ContactDto> contacts = null)
		{
			Id = Guid.Parse(id);
			AuthorizedName = authorizedName;
			AuthorizedLastName = authorizedLastName;
			CompanyName = companyName;
			CreatedDate = createdDate;
			Contacts = contacts;
		}
		public GetHotelResult()
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
