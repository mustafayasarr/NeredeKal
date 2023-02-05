using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NeredeKal.HotelServices.Domain.Constants;
using NeredeKal.HotelServices.Domain.Models.Messaging.Queries.Hotel;
using NeredeKal.HotelServices.Domain.Models.Results;
using NeredeKal.HotelServices.Domain.Models.Results.Hotel;
using NeredeKal.HotelServices.Infrastructure.Repositories.Abstract;

namespace NeredeKal.HotelServices.Core.Services.Hotel
{
	public class GetHotelHandler : IRequestHandler<GetHotelQuery, BaseResponseResult<GetHotelResult>>
	{
		private readonly ILogger<GetHotelHandler> _logger;
		private IUnitOfWork _unitOfWork;
		public GetHotelHandler(IUnitOfWork unitOfWork, ILogger<GetHotelHandler> logger)
		{
			_unitOfWork = unitOfWork;
			_logger = logger;
		}
		public async Task<BaseResponseResult<GetHotelResult>> Handle(GetHotelQuery request, CancellationToken cancellationToken)
		{
			var response = new BaseResponseResult<GetHotelResult>();
			try
			{
				var data = await (from item in _unitOfWork.HotelRepository.Table.Include(x => x.Contacts)
								  where item.Id == request.HotelId
								  select new GetHotelResult()
								  {
									  Id = item.Id,
									  AuthorizedName = item.AuthorizedName,
									  AuthorizedLastName = item.AuthorizedLastName,
									  CompanyName = item.CompanyName,
									  CreatedDate = item.CreatedAtUTC,
									  Contacts = item.Contacts.Select(x => new Domain.Models.Dtos.ContactDto(x.Id, x.ContactType, x.Content, x.CreatedAtUTC)).ToList(),

								  }).FirstOrDefaultAsync();

				if (data != null)
				{
					response.Result = data;
				}

			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				response.Errors.Add(ResponseMessageConstants.AnErrorOccurred);
			}
			return response;
		}
	}
}
