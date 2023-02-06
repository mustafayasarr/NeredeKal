namespace NeredeKal.HotelServices.Domain.Models.Entities
{
	public class Hotel : EntityBase<Guid>
	{
		public Hotel(string authorizedName, string authorizedLastName, string companyName)
		{
			AuthorizedName = authorizedName;
			AuthorizedLastName = authorizedLastName;
			CompanyName = companyName;

		}
		public Hotel()
		{
			Contacts = new List<Contact>();

		}
		public string AuthorizedName { get; set; }
		public string AuthorizedLastName { get; set; }
		public string CompanyName { get; set; }
		public ICollection<Contact> Contacts { get; set; }
	}
}
