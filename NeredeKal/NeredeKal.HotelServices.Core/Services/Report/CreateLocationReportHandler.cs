using MediatR;
using NeredeKal.HotelServices.Domain.Models.Messaging.Commands.Report;
using NeredeKal.HotelServices.Domain.Models.Results.Report;
using NeredeKal.HotelServices.Domain.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NeredeKal.HotelServices.Infrastructure.Repositories.Abstract;
using NeredeKal.HotelServices.Domain.Constants;
using NeredeKal.HotelServices.Domain.Models.Dtos;
using NeredeKal.HotelServices.Domain.Models.Enums;

namespace NeredeKal.HotelServices.Core.Services.Report
{
	public sealed class CreateLocationReportHandler : IRequestHandler<CreateLocationReportCommand, BaseResponseResult<LocationReportResult>>
	{
		private readonly ILogger<CreateLocationReportHandler> _logger;
		private IUnitOfWork _unitOfWork;
		public CreateLocationReportHandler(IUnitOfWork unitOfWork, ILogger<CreateLocationReportHandler> logger)
		{
			_unitOfWork = unitOfWork;
			_logger = logger;
		}
		public async Task<BaseResponseResult<LocationReportResult>> Handle(CreateLocationReportCommand request, CancellationToken cancellationToken)
		{
			var response = new BaseResponseResult<LocationReportResult>();
			try
			{
				int idCount = 0;
				var result = new LocationReportResult
				{
					Id = request.Id,
					ReportName = request.ReportName,
					CreatedDate = DateTime.UtcNow
				};

				var dataList = new List<ReportItemDto>();

				_unitOfWork.ContactRepository.Table.Where(x => x.ContactType == ContactType.Location && (string.IsNullOrEmpty(request.Location) || x.Content == request.Location.ToUpper())).Select(x => x.Content).Distinct().ToList().ForEach(location =>
				{
					var hotels = _unitOfWork.HotelRepository.Table.Where(x => x.Contacts.Any(a => a.ContactType == ContactType.Location && a.Content == location));

					dataList.Add(new ReportItemDto()
					{
						Id = ++idCount,
						Location = location,
						HotelCount = hotels.Count(),
						PhoneCount = hotels.SelectMany(nq => nq.Contacts).Where(nq => nq.ContactType == ContactType.PhoneNumber).Count()
					});

				});
				result.ReportItems = dataList;

				response.Result = result;

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
