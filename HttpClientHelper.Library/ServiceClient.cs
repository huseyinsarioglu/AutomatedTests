namespace HttpClientHelper.Library;

public class ServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly Task<string>? _token;
    private readonly ILogger<ServiceClient>? _logger;

    private StringBuilder? responseText;
    private HttpStatusCode? responseStatusCode;

    public ServiceClient(string baseUrl, Task<string>? token = null) : this(HttpClientManager.Instance.Get(baseUrl), token)
    {
    }

    public ServiceClient(HttpClient httpClient, Task<string>? token = null)
    {
        _httpClient = httpClient;
        _token = token;
        _logger = ApplicationLogging.CreateLogger<ServiceClient>();
    }

    public async Task<ServiceClientResult<TResponse>> CallApiAsync<TResponse>(IRequestContent model, CancellationToken cancellationToken = default)
    {
        responseText = new StringBuilder();
        responseStatusCode = default;

        TResponse? responseObject = default;
        HttpResponseHeaders? responseHeaders = default;
        long? elapsedMilliseconds = default;


        Exception? ex = default;
        try
        {
            _logger?.LogInformation("CallApiAsync started");
            await SetTokenAsync();

            using var httpRequest = model.CreateRequestContent(_httpClient);
            _logger?.LogInformation($"httpRequest.uri: {httpRequest.RequestUri}");

            
            var timer = Stopwatch.StartNew();
            using var response = await _httpClient.SendAsync(
                                        httpRequest,
                                        HttpCompletionOption.ResponseHeadersRead,
                                        cancellationToken);
            timer.Stop();
            elapsedMilliseconds = timer.ElapsedMilliseconds;

            _logger?.LogInformation("Extracting Response Text");
            responseStatusCode = response.StatusCode;
            responseText.Append(await response.Content.ReadAsStringAsync(cancellationToken));
            _logger?.LogInformation($"Extracted Response Text: {responseText}");
            responseHeaders = response.Headers;

            _logger?.LogInformation("Extracting Response Object");
            responseObject = ReadObjectResponseAsync<TResponse>();
            _logger?.LogInformation("Extracted Response Object");
        }
        catch (Exception exc)
        {
            _logger?.LogError(exc, "An error occured in HttpClientHelper!");
            ex = exc;
        }

        return new ServiceClientResult<TResponse>(responseObject, responseText.ToString(), responseStatusCode, responseHeaders, elapsedMilliseconds ?? 0, ex, model, _httpClient.BaseAddress);
    }

    public Task<ServiceClientResult<TResponse>> GetAsync<TResponse>(string url, CancellationToken cancellationToken = default)
    {
        var requestContent = RequestContentFactory.CreateRequestContent(HttpMethod.Get, url);
        return CallApiAsync<TResponse>(requestContent, cancellationToken);
    }

    public Task<ServiceClientResult<TResponse>> PostAsync<TRequest, TResponse>(TRequest? model, string url, CancellationToken cancellationToken = default)
    {
        var requestContent = RequestContentFactory.CreateRequestContent(model, HttpMethod.Post, url);
        return CallApiAsync<TResponse>(requestContent, cancellationToken);
    }

    private async Task SetTokenAsync()
    {
        _httpClient.DefaultRequestHeaders.Authorization = _token != null
                                                        ? new AuthenticationHeaderValue("Bearer", await _token)
                                                        : _httpClient.DefaultRequestHeaders.Authorization;
    }

    private T? ReadObjectResponseAsync<T>()
    {
        var text = responseText?.ToString();

        if (!IsSuccessStatusCode() || string.IsNullOrEmpty(text)) 
        {
            return default;
        }

        return typeof(T) == typeof(string)
             ? (T?)(object?)(text)
             : JsonSerializer.Deserialize<T>(text, JsonSerializerOptionsManager.Options);
    }

    private bool IsSuccessStatusCode()
    {
        int intResponseStatusCode = responseStatusCode.HasValue ? (int)responseStatusCode.Value : (int)decimal.Zero;
        return intResponseStatusCode >= 200 && intResponseStatusCode <= 299;
    }
}
