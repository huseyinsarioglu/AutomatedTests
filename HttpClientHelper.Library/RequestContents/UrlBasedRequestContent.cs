
namespace HttpClientHelper.Library.RequestContents;

public class UrlBasedRequestContent<T> : JsonRequestContent<T>
{
    public UrlBasedRequestContent(RequestContentCtor<T> ctor) : base(ctor)
    {
    }

    protected override void ProcessRequestContent(HttpClient httpClient, HttpRequestMessage httpRequest)
    {
        string seperator = base.RequestContentCtor.Url.Contains('?') ? "&" : "?";
        var contentDictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(RequestContentJson, JsonSerializerOptionsManager.Options);

        var contentStringParts = contentDictionary?.Select(x => HttpUtility.UrlEncode(x.Key) + "=" + HttpUtility.UrlEncode(x.Value)) ?? Array.Empty<string>();
        var urlSuffix = string.Join("&", contentStringParts);

        ChangeUrl(base.RequestContentCtor.Url + seperator + urlSuffix);
        RequestContentJson = string.Empty;
    }
}
