using NeredeKal.ReportServices.Domain.Models.Enums;

namespace NeredeKal.ReportServices.Domain.Models.Results.Report
{
	public class GetReportResult
	{
		public int Id { get; set; }
		public string? Path { get; set; }
		public ReportStatus Status { get; set; }
		public DateTime CreatedDate { get; set; }
	}
}
