using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NeredeKal.HotelServices.Domain.Constants;
using NeredeKal.HotelServices.Domain.Models.Messaging.Queries.Hotel;
using NeredeKal.HotelServices.Domain.Models.Results;
using NeredeKal.HotelServices.Domain.Models.Results.Hotel;
using NeredeKal.HotelServices.Infrastructure.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeredeKal.HotelServices.Core.Services.Hotel
{
	public class GetAllHotelHandler : IRequestHandler<GetAllHotelQuery, BaseResponseResult<List<GetAllHotelResult>>>
	{
		private readonly ILogger<GetAllHotelHandler> _logger;
		private IUnitOfWork _unitOfWork;
		public GetAllHotelHandler(IUnitOfWork unitOfWork, ILogger<GetAllHotelHandler> logger)
		{
			_unitOfWork = unitOfWork;
			_logger = logger;
		}
		public async Task<BaseResponseResult<List<GetAllHotelResult>>> Handle(GetAllHotelQuery request, CancellationToken cancellationToken)
		{
			var response = new BaseResponseResult<List<GetAllHotelResult>>();
			try
			{
				response.Result = await _unitOfWork.HotelRepository.Table.Include(x => x.Contacts)
					.Select(x => new GetAllHotelResult
					(x.Id.ToString(), x.AuthorizedName, x.AuthorizedLastName, x.CompanyName, x.CreatedAtUTC,
					x.Contacts.Select(a => new Domain.Models.Dtos.ContactDto
					{ Id = a.Id, CreatedDate = a.CreatedAtUTC, ContactContent = a.Content, ContactType = a.ContactType }
					)
					.ToList())).ToListAsync();

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
