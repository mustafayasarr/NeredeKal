using MediatR;
using NeredeKal.ReportServices.Domain.Models.Dtos;
using NeredeKal.ReportServices.Domain.Models.Enums;
using NeredeKal.ReportServices.Domain.Models.Results;

namespace NeredeKal.ReportServices.Domain.Models.Messaging.Commands
{
	public class UpdateLocationReportCommand : IRequest<BaseResponseResult>
	{
		public Guid? Id { get; set; }
		public string? ReportName { get; set; }
		public DateTime? CreatedDate { get; set; }
		public List<ReportItemDto>? ReportItems { get; set; }
		public string? JsonRequest { get; set; }
		public ReportStatus? Status { get; set; }
		public string? Message { get; set; }
	}
}
