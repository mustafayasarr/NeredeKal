namespace NeredeKal.HotelServices.Domain.Models.Events
{
	public class CreateReportEvent
	{
		public Guid Id { get; set; }
		public string ReportName { get; set; }
		public string Location { get; set; }
	}
}
