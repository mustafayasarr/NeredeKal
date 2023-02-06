using NeredeKal.ReportServices.Domain.Models.Enums;
using Newtonsoft.Json;

namespace NeredeKal.ReportServices.Domain.Models.Results
{
	public class BaseQueueResult<T>
	{
		public ReportStatus Status { get; set; }

		public string? JsonObject { get; set; }
		public T? RequestObject
		{
			get => string.IsNullOrEmpty(JsonObject) ? default : JsonConvert.DeserializeObject<T>(JsonObject);
			set => JsonObject = JsonConvert.SerializeObject(value);
		}
	}

}
