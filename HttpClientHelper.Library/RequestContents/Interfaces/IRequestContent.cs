namespace HttpClientHelper.Library.RequestContents.Interfaces;

public interface IRequestContent
{
    string RequestContentJson { get; }

    string ContentType { get; }

    HttpRequestMessage CreateRequestContent(HttpClient httpClient);

    RequestContentCtor RequestContentCtor { get; }
}

