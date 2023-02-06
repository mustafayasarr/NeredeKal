namespace NeredeKal.HotelServices.Core.Gateways
{
	public interface IRestService
	{
		Task<T> PostMethodAsync<T>(object obj, string uri, Dictionary<string, string> headers = null);
		Task<T> GetMethodAsync<T>(string uri, Dictionary<string, string> headers = null);
		Task<T> PutMethodAsync<T>(object obj, string uri, Dictionary<string, string> headers = null);
	}
}
