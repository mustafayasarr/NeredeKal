using MediatR;
using NeredeKal.ReportServices.Domain.Models.Dtos;
using NeredeKal.ReportServices.Domain.Models.Results;

namespace NeredeKal.ReportServices.Domain.Models.Messaging.Queries
{
	public class GetListReportQuery : IRequest<BaseResponseResult<List<GetReportResult>>>
	{
	}
}
