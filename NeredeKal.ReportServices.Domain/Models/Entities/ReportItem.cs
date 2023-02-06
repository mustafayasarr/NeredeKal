using NeredeKal.ReportServices.Domain.Models.Enums;

namespace NeredeKal.ReportServices.Domain.Models.Entities
{
	public class ReportItem : EntityBase<Guid>
	{
		public ReportItem(string? reportName, ReportStatus status)
		{
			ReportName = reportName;
			Status = status;
		}
		public ReportItem()
		{

		}
		public string? ReportName { get; set; }
		public string? Path { get; set; }
		public string? RequestObjectJson { get; set; }
		public ReportStatus Status { get; set; }
		public DateTime? ReportCreateDate { get; set; }
		public string? Message { get; set; }

	}
}
