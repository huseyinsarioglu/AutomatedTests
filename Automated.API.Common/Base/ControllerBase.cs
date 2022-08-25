namespace Automated.API.Common.Base;

public abstract class ControllerBase
{
    protected readonly ServiceClient _serviceClient;
    private readonly UrlListBase _urlList;

    public ControllerBase(UrlListBase urlList)
    {
        _urlList = urlList;
        _serviceClient = new ServiceClient(_urlList.ApiBaseUrl);
    }

    public ControllerBase(HttpClient httpClient, UrlListBase urlList)
    {
        _urlList = urlList;
        _serviceClient = new ServiceClient(httpClient);
    }

    protected string ControllerRoute => _urlList.GetUrl(this.GetType().Name);

    protected Task<ServiceClientResult<TResponse>> GetApiAsync<TResponse>(string url = "")
    {
        return _serviceClient.GetAsync<TResponse>(GetUrl(url));
    }

    protected Task<ServiceClientResult<TResponse>> CallApiAsync<TResponse>(HttpMethod httpMethod, string? actionUrl = default, bool isRequestParameterFromUri = false, Dictionary<string, string>? headerValues = null)
    {
        return CallApiAsync<object, TResponse>(httpMethod, actionUrl, request: null, isRequestParameterFromUri, headerValues);
    }

    protected Task<ServiceClientResult<TResponse>> CallApiAsync<TRequest, TResponse>(HttpMethod httpMethod, string? actionUrl = default, TRequest? request = default, bool isRequestParameterFromUri = false, Dictionary<string, string>? headerValues = null)
    {
        var model = RequestContentFactory.CreateRequestContent(request, httpMethod, GetUrl(actionUrl ?? string.Empty), isRequestParameterFromUri, headerValues);
        return _serviceClient.CallApiAsync<TResponse>(model);
    }

    private string GetUrl(string? actionUrl)
    {
        return $"{_urlList.ApiBaseSuffix}/{ControllerRoute}/{actionUrl}".Replace("//", "/");
    }
}
