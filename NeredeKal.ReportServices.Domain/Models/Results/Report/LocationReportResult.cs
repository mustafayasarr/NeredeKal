using NeredeKal.ReportServices.Domain.Models.Dtos;

namespace NeredeKal.ReportServices.Domain.Models.Results.Report
{
	public class LocationReportResult
	{
		public LocationReportResult()
		{
			ReportItems = new List<ReportItemDto>();
		}
		public int Id { get; set; }
		public string ReportName { get; set; }
		public DateTime CreatedDate { get; set; }
		public List<ReportItemDto> ReportItems { get; set; }
	}
}
