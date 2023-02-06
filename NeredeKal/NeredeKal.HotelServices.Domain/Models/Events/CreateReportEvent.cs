using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeredeKal.HotelServices.Domain.Models.Events
{
	public class CreateReportEvent
	{
		public int Id { get; set; }
		public string ReportName { get; set; }
		public string Location { get; set; }
	}
}
