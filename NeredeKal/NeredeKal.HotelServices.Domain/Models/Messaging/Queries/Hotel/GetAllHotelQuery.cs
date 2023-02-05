using MediatR;
using NeredeKal.HotelServices.Domain.Models.Results;
using NeredeKal.HotelServices.Domain.Models.Results.Hotel;

namespace NeredeKal.HotelServices.Domain.Models.Messaging.Queries.Hotel
{

	public sealed record class GetAllHotelQuery : IRequest<BaseResponseResult<List<GetAllHotelResult>>>
	{
	}
}
