using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System;
using RestSharp;
using Microsoft.Extensions.Logging;
using LaxmiSunriseBank.Services.API.APIHandler;
using LaxmiSunriseBank.Services.Models;
using LaxmiSunriseBank.Services.Models.Constants;
using Microsoft.Extensions.Configuration;


public class APIHandler : IAPIHandler
{
    #region Local Variables
    private readonly RestClient _client;
    private readonly ILogger<APIHandler> _logger;
    private readonly int _apiTimeOut;
    #endregion

    #region Constructor
    /// <summary>
    /// Parameterized Constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="configuration"></param>
    public APIHandler(ILogger<APIHandler> logger, IConfiguration configuration)
    {
        _client = new RestClient();
        _logger = logger;
        string apiTimeoutValue = configuration.GetSection("AppSettings")["APITimeout"];
        int defaultAPITimeout = 60000;
        _apiTimeOut = !string.IsNullOrEmpty(apiTimeoutValue) && int.TryParse(apiTimeoutValue, out int value)
            ? value : defaultAPITimeout;
    }
    #endregion

    #region Soap Call
    /// <summary>
    /// SOAP Call
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="endpoint"></param>
    /// <param name="requestPayload"></param>
    /// <returns></returns>
    public async Task<CommonAPIResponseModel<T>> SOAPPostCall<T>(string endpoint, string requestPayload)
    {
        var tcs = new TaskCompletionSource<CommonAPIResponseModel<T>>();
        Uri fullUri = new Uri(endpoint);
        string baseUrl = $"{fullUri.Scheme}://{fullUri.Host}";
        string resourceUrl = fullUri.PathAndQuery;
        _client.BaseUrl = new Uri(baseUrl);
        _logger.LogInformation("API Soap Request calling to {endpoint}{BankCode}",endpoint,GeneralTermConstants.ClientCode);
        var request = new RestRequest(resourceUrl, Method.POST);
        request.Timeout = _apiTimeOut;
        request.AddHeader("Content-Type", "text/xml;");
        request.AddParameter("text/xml", requestPayload, ParameterType.RequestBody);
        _client.ExecuteAsync<T>(request, (response, handle) =>
        {
            if (response.IsSuccessful)
            {
                var returnData = new CommonAPIResponseModel<T>
                {
                    IsSuccess = true,
                    ResponseData = response.Data!,
                    Response = response.Content
                };
                tcs.SetResult(returnData);
            }
            else
            {
                var returnData = new CommonAPIResponseModel<T>
                {
                    IsSuccess = false,
                    ErrorMessage = response.ErrorMessage
                };
                if (response.ResponseStatus == ResponseStatus.TimedOut)
                {
                    returnData.IsTimedOut = true;
                }
                LogError(response, endpoint).Wait();
                tcs.SetResult(returnData);
            }
        }, Method.POST);

        return await tcs.Task;
    }
    #endregion

    #region Get Async
    /// <summary>
    /// Get Http Call 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="endpoint"></param>
    /// <returns></returns>
    /// <exception cref="HttpRequestException"></exception>
    public Task<T> GetAsync<T>(string endpoint)
    {
        var tcs = new TaskCompletionSource<T>();

        var request = new RestRequest(endpoint, Method.GET);
        request.Timeout = _apiTimeOut;

        _logger.LogInformation("API Get Request calling to {endpoint}{BankCode}",endpoint,GeneralTermConstants.ClientCode);

        _client.ExecuteAsync<T>(request, (response, handle) =>
        {
            if (response.IsSuccessful)
            {
                tcs.SetResult(response.Data!);
            }
            else
            {
                LogError(response, endpoint).Wait();
            }
        }, Method.GET);

        return tcs.Task;
    }
    #endregion

    #region Post Async
    /// <summary>
    /// Post HTTP Call
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TRequest"></typeparam>
    /// <param name="endpoint"></param>
    /// <param name="requestPayload"></param>
    /// <returns>T</returns>
    /// <exception cref="HttpRequestException"></exception>
    public Task<T> PostAsync<T, TRequest>(string endpoint, TRequest requestPayload) where TRequest : class
    {
        var tcs = new TaskCompletionSource<T>();

        _logger.LogInformation("API Post Request calling to {endpoint}, body {body}{BankCode}",endpoint, JsonSerializer.Serialize(requestPayload), GeneralTermConstants.ClientCode);

        var request = new RestRequest(endpoint, Method.POST);
        request.Timeout = _apiTimeOut;
        request.AddJsonBody(requestPayload);

        _client.ExecuteAsync<T>(request, (response, handle) =>
        {
            if (response.IsSuccessful)
            {
                tcs.SetResult(response.Data!);
            }
            else
            {
                LogError(response, endpoint).Wait();
            }
        }, Method.POST);

        return tcs.Task;
    }
    #endregion

    #region PUT Async
    /// <summary>
    /// Put HTTP Call
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TRequest"></typeparam>
    /// <param name="endpoint"></param>
    /// <param name="requestPayload"></param>
    /// <returns></returns>
    /// <exception cref="HttpRequestException"></exception>
    public Task<T> PutAsync<T, TRequest>(string endpoint, TRequest requestPayload) where TRequest : class
    {
        var tcs = new TaskCompletionSource<T>();

        _logger.LogInformation("API Put Request calling to {endpoint}, body {body}{BankCode}",endpoint, JsonSerializer.Serialize(requestPayload),GeneralTermConstants.ClientCode);

        var request = new RestRequest(endpoint, Method.PUT);
        request.Timeout = _apiTimeOut;
        request.AddJsonBody(requestPayload);

        _client.ExecuteAsync<T>(request, (response, handle) =>
        {
            if (response.IsSuccessful)
            {
                tcs.SetResult(response.Data!);
            }
            else
            {
                LogError(response, endpoint).Wait();
            }
        }, Method.PUT);

        return tcs.Task;
    }
    #endregion

    #region Delete Async
    /// <summary>
    /// Delete HTTP Call
    /// </summary>
    /// <param name="endpoint"></param>
    /// <returns></returns>
    /// <exception cref="HttpRequestException"></exception>
    public Task DeleteAsync(string endpoint)
    {
        var tcs = new TaskCompletionSource<bool>();

        _logger.LogInformation("API Delete Request calling to {endpoint}{BankCode}",endpoint,GeneralTermConstants.ClientCode);

        var request = new RestRequest(endpoint, Method.DELETE);
        request.Timeout = _apiTimeOut;

        _client.ExecuteAsync(request, (response, handle) =>
        {
            if (response.IsSuccessful)
            {
                tcs.SetResult(true);
            }
            else
            {
                LogError(response, endpoint).Wait();
            }
        }, Method.DELETE);

        return tcs.Task;
    }
    #endregion

    #region Patch Async
    /// <summary>
    /// Patch HTTP Call
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TRequest"></typeparam>
    /// <param name="endpoint"></param>
    /// <param name="requestPayload"></param>
    /// <returns></returns>
    /// <exception cref="HttpRequestException"></exception>
    public Task<T> PatchAsync<T, TRequest>(string endpoint, TRequest requestPayload) where TRequest : class
    {
        var tcs = new TaskCompletionSource<T>();

        _logger.LogInformation("API Patch Request calling to {endpoint}{BankCode}",endpoint,GeneralTermConstants.ClientCode);

        var request = new RestRequest(endpoint, Method.PATCH);
        request.Timeout = _apiTimeOut;
        request.AddJsonBody(requestPayload);

        _client.ExecuteAsync<T>(request, (response, handle) =>
        {
            if (response.IsSuccessful)
            {
                tcs.SetResult(response.Data!);
            }
            else
            {
                LogError(response, endpoint).Wait();
            }
        }, Method.PATCH);

        return tcs.Task;
    }
    #endregion

    #region Log Error
    /// <summary>
    /// Log Error
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    private async Task LogError(IRestResponse response, string endPoint)
    {
        _logger.LogError("API Error occurred: end point {endPoint} status code {StatusCode} error message {ErrorMessage}{BankCode}",endPoint,response.StatusCode,response.ErrorMessage,GeneralTermConstants.ClientCode);
        if (response.Content != null)
        {
            _logger.LogError("API Error response content {content}{BankCode}",response.Content,GeneralTermConstants.ClientCode);
        }
        await Task.CompletedTask;
    }
    #endregion
}
