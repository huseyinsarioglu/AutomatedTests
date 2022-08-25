namespace HttpClientHelper.Library.RequestContents;

public class JsonRequestContent<T> : RequestContentBase<T>
{
    //public JsonRequestContent(T? request, string url, HttpMethod httpMethod) : base(request, url, httpMethod)
    public JsonRequestContent(RequestContentCtor<T> ctor) : base(ctor)
    {
    }

    public override string ContentType => "application/json; charset=utf-8";

    protected override void ProcessRequestContent(HttpClient httpClient, HttpRequestMessage httpRequest)
    {
        httpRequest.Content = new StringContent(RequestContentJson);
    }
}
