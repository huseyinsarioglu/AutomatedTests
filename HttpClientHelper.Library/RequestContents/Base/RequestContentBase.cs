namespace HttpClientHelper.Library.RequestContents.Base;

public record RequestContentCtor(string Url, HttpMethod HttpMethod, Dictionary<string, string>? HeaderValues);

public record RequestContentCtor<T>(T? Request, string Url, HttpMethod HttpMethod, Dictionary<string, string>? HeaderValues) : RequestContentCtor(Url, HttpMethod, HeaderValues);

public abstract class RequestContentBase<T> : IRequestContent
{
    protected readonly T? _request;

    // var requestContent = JsonConvert.SerializeObject(_request, JsonSerializerSettings);

    public RequestContentBase(RequestContentCtor<T> ctor)
    {
        _request = ctor.Request;
        RequestContentCtor = ctor;
    }

    protected void ChangeUrl(string newUrl)
    {
        RequestContentCtor = new RequestContentCtor<T>(_request, newUrl, this.RequestContentCtor.HttpMethod, this.RequestContentCtor.HeaderValues);
    }

    public string RequestContentJson { get; protected set; } = string.Empty;

    public abstract string ContentType { get; }

    public RequestContentCtor RequestContentCtor { get; private set; }

    protected abstract void ProcessRequestContent(HttpClient httpClient, HttpRequestMessage httpRequest);

    protected virtual void SetRequestContentJson()
    {
        RequestContentJson = JsonSerializer.Serialize(_request, JsonSerializerOptionsManager.Options);
    }

    public virtual HttpRequestMessage CreateRequestContent(HttpClient httpClient)
    {
        var url = RequestContentCtor.Url;
        ArgumentNullException.ThrowIfNull(url, nameof(url));

        var httpRequest = new HttpRequestMessage
        {
            Method = RequestContentCtor.HttpMethod
        };

        httpRequest.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse(ContentType));

        if (_request != null)
        {
            SetRequestContentJson();
            ProcessRequestContent(httpClient, httpRequest);

            if (httpRequest.Content != null)
            {
                httpRequest.Content.Headers.ContentType = MediaTypeHeaderValue.Parse(ContentType);
            }
        }

        httpRequest.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);

        foreach (var item in RequestContentCtor.HeaderValues ?? new Dictionary<string, string>())
        {
            httpRequest.Headers.Add(item.Key, item.Value);
        }

        return httpRequest;
    }
}
