using NeredeKal.HotelServices.Domain.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeredeKal.HotelServices.Domain.Models.Results.Report
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
