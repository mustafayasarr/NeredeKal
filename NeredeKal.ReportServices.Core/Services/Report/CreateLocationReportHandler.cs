using DotNetCore.CAP;
using MediatR;
using Microsoft.Extensions.Logging;
using NeredeKal.ReportServices.Domain.Constants;
using NeredeKal.ReportServices.Domain.Models.Enums;
using NeredeKal.ReportServices.Domain.Models.Messaging.Commands;
using NeredeKal.ReportServices.Domain.Models.Results;
using NeredeKal.ReportServices.Infrastructure.Repositories.Abstract;

namespace NeredeKal.ReportServices.Core.Services.Report
{
	public class CreateLocationReportHandler : IRequestHandler<CreateLocationReportCommand, BaseResponseResult>
	{
		private readonly ILogger<CreateLocationReportHandler> _logger;
		private readonly ICapPublisher _capPublisher;
		private readonly IUnitOfWork _unitOfWork;

		public CreateLocationReportHandler(ICapPublisher capPublisher, ILogger<CreateLocationReportHandler> logger, IUnitOfWork unitOfWork)
		{

			_logger = logger;
			_capPublisher = capPublisher;
			_unitOfWork = unitOfWork;

		}
		public async Task<BaseResponseResult> Handle(CreateLocationReportCommand request, CancellationToken cancellationToken)
		{
			var response = new BaseResponseResult();
			try
			{
				var entity = new Domain.Models.Entities.ReportItem(request.ReportName, ReportStatus.Bekliyor);
				await _unitOfWork.ReportItemRepository.CreateAsync(entity);
				await _unitOfWork.CompleteAsync();
				request.Id = entity.Id;
				await _capPublisher.PublishAsync(ContactSubscriberConstant.CreateReportQueue, request);
			}
			catch (Exception ex)
			{
				await _unitOfWork.CompleteAsync(false);
				_logger.LogError(ex, ex.Message);
				response.Errors.Add(ResponseMessageConstants.AnErrorOccurred);
			}
			return response;
		}
	}
}
