using Microsoft.AspNetCore.Http;
namespace NeredeKal.HotelServices.Core.Gateways
{
	public class BaseService
	{
		protected string BaseAddress;
		protected string ServiceSecret;
		protected readonly IRestService _restService;
		private readonly IHttpContextAccessor _httpContextAccessor;
		protected Dictionary<string, string> RequestHeader;
		public BaseService(IRestService restService, IHttpContextAccessor httpContextAccessor)
		{
			_restService = restService;
			_httpContextAccessor = httpContextAccessor;
		}
		public HttpContext HttpContext => _httpContextAccessor.HttpContext;
		protected Dictionary<string, string> RequestDefaultHeader
		{
			get
			{
				var correlationId = HttpContext?.Items["CorrelationId"].ToString();
				RequestHeader = new Dictionary<string, string> { { "Authorization", $"Basic {ServiceSecret}" }, { "CorrelationId", correlationId } };
				return RequestHeader;
			}
		}
	}
}
