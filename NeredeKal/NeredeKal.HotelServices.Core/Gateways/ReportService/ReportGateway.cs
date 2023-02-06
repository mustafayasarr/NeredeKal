using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using NeredeKal.HotelServices.Domain.Constants;
using NeredeKal.HotelServices.Domain.Models.Dtos;
using NeredeKal.HotelServices.Domain.Models.Results;

namespace NeredeKal.HotelServices.Core.Gateways.ReportService
{
	public class ReportGateway : BaseService, IReportGateway
	{
		public ReportGateway(IRestService restService, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(restService, httpContextAccessor)
		{
			BaseAddress = configuration["ServiceUrls:ReportService"];
		}

		public async Task<BaseResponseResult> UpdateReport(UpdateLocationReportDto request)
		{
			return await _restService.PostMethodAsync<BaseResponseResult>(request, BaseAddress + GatewayUrls.UpdateReport);
		}

	}
}
