using NeredeKal.HotelServices.Domain.Models.Enums;

namespace NeredeKal.HotelServices.Domain.Models.Dtos
{
	public class UpdateLocationReportDto
	{
		public UpdateLocationReportDto()
		{
			ReportItems = new List<ReportItemDto>();
		}
		public int? Id { get; set; }
		public string? ReportName { get; set; }
		public DateTime? CreatedDate { get; set; }
		public List<ReportItemDto> ReportItems { get; set; }
		public string? JsonRequest { get; set; }
		public ReportStatus Status { get; set; }
		public string? Message { get; set; }
	}
}
