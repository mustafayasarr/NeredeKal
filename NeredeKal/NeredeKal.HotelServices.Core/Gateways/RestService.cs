using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using RestSharpPolly;
using RestSharpPolly.PolicyProviders;

namespace NeredeKal.HotelServices.Core.Gateways
{
	public class RestService : IRestService
	{
		private ILogger<RestService> _logger;
		public RestService(ILogger<RestService> logger)
		{
			_logger = logger;
		}

		public async Task<T> PostMethodAsync<T>(object obj, string uri, Dictionary<string, string> headers = null)
		{
			var request = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json };
			var result = await GetResult<T>(uri, request, obj, headers);

			return result;
		}

		public async Task<T> GetMethodAsync<T>(string uri, Dictionary<string, string> headers = null)
		{
			var request = new RestRequest(Method.GET) { RequestFormat = DataFormat.Json };
			var result = await GetResult<T>(uri, request, null, headers);

			return result;
		}

		public async Task<T> PutMethodAsync<T>(object obj, string uri, Dictionary<string, string> headers = null)
		{
			var request = new RestRequest(Method.PUT) { RequestFormat = DataFormat.Json };
			var result = await GetResult<T>(uri, request, obj, headers);

			return result;
		}
		private async Task<T> GetResult<T>(string uri, RestRequest request, object obj = null, Dictionary<string, string> headers = null)
		{
			try
			{
				_logger.LogInformation("RestService request Uri:" + uri);
				var client = RestClientFactory<IRestResponse>.Create(TimeoutAndRetryAsyncPolicy.Build(1, 1, 60));
				client.BaseUrl = new Uri(uri);

				client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

				if (headers != null)
				{
					foreach (var header in headers)
					{
						request.AddHeader(header.Key, header.Value);
					}
				}

				if (obj != null)
				{
					request.AddJsonBody(obj);
				}

				var response = await client.ExecuteAsync(request);
				if (response?.ErrorException != null)
				{
					throw new Exception(response?.ErrorException?.Message);
				}


				return JsonConvert.DeserializeObject<T>(response.Content,
					new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
