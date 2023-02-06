using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeredeKal.HotelServices.Domain.Models.Dtos
{
	public class ReportItemDto
	{
		public int Id { get; set; }
		public string Location { get; set; }
		public int HotelCount { get; set; }
		public int PhoneCount { get; set; }
	}
}
