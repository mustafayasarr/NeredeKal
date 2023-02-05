using MediatR;
using NeredeKal.HotelServices.Domain.Models.Results;
using NeredeKal.HotelServices.Domain.Models.Results.Hotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeredeKal.HotelServices.Domain.Models.Messaging.Queries.Hotel
{
	public sealed record class GetHotelQuery : IRequest<BaseResponseResult<GetHotelResult>>
	{
		public GetHotelQuery(string hotelId)
		{
			HotelId = Guid.Parse(hotelId);
		}
		public Guid HotelId { get; set; }
	}
}
