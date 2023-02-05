using MediatR;
using NeredeKal.HotelServices.Domain.Models.Dtos;
using NeredeKal.HotelServices.Domain.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeredeKal.HotelServices.Domain.Models.Messaging.Commands.Hotel
{
	public class CreateHotelCommand : IRequest<BaseResponseResult>
	{
		public CreateHotelCommand()
		{
			Contacts = new List<ContactDto>();
		}
		public string AuthorizedName { get; set; }
		public string AuthorizedLastName { get; set; }
		public string CompanyName { get; set; }
		public List<ContactDto> Contacts { get; set; }
	}
}
