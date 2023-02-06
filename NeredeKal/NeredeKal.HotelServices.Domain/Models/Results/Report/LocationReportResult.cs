using NeredeKal.HotelServices.Domain.Models.Dtos;

namespace NeredeKal.HotelServices.Domain.Models.Results.Report
{
	public class LocationReportResult
	{
		public LocationReportResult()
		{
			ReportItems = new List<ReportItemDto>();
		}
		public Guid Id { get; set; }
		public string ReportName { get; set; }
		public DateTime CreatedDate { get; set; }
		public List<ReportItemDto> ReportItems { get; set; }
	}
}
