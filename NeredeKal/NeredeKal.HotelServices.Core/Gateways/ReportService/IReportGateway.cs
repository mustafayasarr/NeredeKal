using NeredeKal.HotelServices.Domain.Models.Dtos;
using NeredeKal.HotelServices.Domain.Models.Results;

namespace NeredeKal.HotelServices.Core.Gateways.ReportService
{
	public interface IReportGateway
	{
		Task<BaseResponseResult> UpdateReport(UpdateLocationReportDto request);
	}
}
