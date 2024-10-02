
using LaxmiSunriseBank.Services.Models;
using System.Threading.Tasks;

namespace LaxmiSunriseBank.Services.API.APIHandler
{
    public interface IAPIHandler
    {
        Task<CommonAPIResponseModel<T>> SOAPPostCall<T>(string endpoint, string requestPayload);
        Task<T> GetAsync<T>(string endpoint);
        Task<T> PostAsync<T, TRequest>(string endpoint, TRequest requestPayload) where TRequest : class;
        Task<T> PutAsync<T, TRequest>(string endpoint, TRequest requestPayload) where TRequest : class;
        Task DeleteAsync(string endpoint);
        Task<T> PatchAsync<T, TRequest>(string endpoint, TRequest requestPayload) where TRequest : class;
    }
}
