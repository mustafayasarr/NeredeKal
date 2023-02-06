using MediatR;
using NeredeKal.ReportServices.Domain.Models.Results;

namespace NeredeKal.ReportServices.Domain.Models.Messaging.Commands
{
	public class CreateLocationReportCommand : IRequest<BaseResponseResult>
	{
		public CreateLocationReportCommand(int id, string reportName, string location)
		{
			ReportName = reportName;
			Location = location;
		}
		public CreateLocationReportCommand()
		{

		}
		public Guid Id { get; set; }
		public string ReportName { get; set; }
		public string? Location { get; set; }

	}
}
