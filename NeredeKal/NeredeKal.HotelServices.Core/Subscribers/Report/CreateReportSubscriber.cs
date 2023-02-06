using DotNetCore.CAP;
using MediatR;
using NeredeKal.HotelServices.Core.Gateways.ReportService;
using NeredeKal.HotelServices.Domain.Models.Dtos;
using NeredeKal.HotelServices.Domain.Models.Enums;
using NeredeKal.HotelServices.Domain.Models.Events;
using NeredeKal.HotelServices.Domain.Models.Messaging.Commands.Report;
using Newtonsoft.Json;

namespace NeredeKal.HotelServices.Core.Subscribers.Report
{
	public class CreateReportSubscriber : ICapSubscribe
	{
		private readonly IMediator _mediator;
		private readonly IReportGateway _reportGateway;
		public CreateReportSubscriber(IMediator mediator, IReportGateway reportGateway)
		{
			_mediator = mediator;
			_reportGateway = reportGateway;
		}

		[CapSubscribe(Domain.Constants.ReportSubscriberConstant.CreateReportQueue)]
		public async Task ReportCreateAsync(CreateReportEvent @event)
		{
			UpdateLocationReportDto request = new UpdateLocationReportDto();
			var response = await _mediator.Send(new CreateLocationReportCommand(@event.Id, @event.ReportName, @event.Location));
			if (response.HasError)
			{
				request.Status = ReportStatus.Hata;
				request.Message = response.Errors.FirstOrDefault();
			}
			else
			{
				request.Status = ReportStatus.Tamamlandi;
				request.CreatedDate = response.Result.CreatedDate;
				request.ReportItems = response.Result.ReportItems;
				request.ReportName = response.Result.ReportName;

			}
			request.Id = @event.Id;
			request.JsonRequest = JsonConvert.SerializeObject(@event);


			await _reportGateway.UpdateReport(request);
		}
	}
}
