using NeredeKal.ReportServices.Domain.Models.Enums;

namespace NeredeKal.ReportServices.Domain.Models.Dtos
{
	public class GetReportResult
	{
		public Guid Id { get; set; }
		public string? Path { get; set; }
		public ReportStatus Status { get; set; }
		public DateTime CreatedDate { get; set; }
	}
}
