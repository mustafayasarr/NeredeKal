using MediatR;
using NeredeKal.HotelServices.Domain.Models.Results.Report;
using NeredeKal.HotelServices.Domain.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeredeKal.HotelServices.Domain.Models.Messaging.Commands.Report
{
	public sealed record class CreateLocationReportCommand : IRequest<BaseResponseResult<LocationReportResult>>
	{
		public CreateLocationReportCommand(int id, string reportName, string location)
		{
			Id = id;
			ReportName = reportName;
			Location = location;
		}
		public CreateLocationReportCommand()
		{

		}
		public int Id { get; set; }
		public string ReportName { get; set; }
		public string Location { get; set; }
	}
}
