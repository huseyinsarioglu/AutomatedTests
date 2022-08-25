namespace HttpClientHelper.Library;

internal class HttpClientManager : IDisposable
{
    private static readonly HttpClientManager _instance = new();

    private bool disposed;
    private readonly List<HttpClient> _httpClients = new();

    private HttpClientManager() { }

    public static HttpClientManager Instance { get { return _instance; } }

    public HttpClient Get(string baseUrl)
    {
        return _httpClients.FirstOrDefault(c => c.BaseAddress == new Uri(baseUrl))
            ?? CreateNew(baseUrl);
    }

    private HttpClient CreateNew(string baseUrl)
    {
        var client = HttpClientFactory.Create();
        client.BaseAddress = new Uri(baseUrl);
        _httpClients.Add(client);
        return client;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                foreach (var httpClient in _httpClients)
                {
                    httpClient.Dispose();
                }

                _httpClients.Clear();
            }

            disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}