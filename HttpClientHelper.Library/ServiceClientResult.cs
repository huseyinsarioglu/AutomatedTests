namespace HttpClientHelper.Library;

public record ServiceClientResult
(
    string RawResponse,
    HttpStatusCode? StatusCode,
    HttpResponseHeaders? Header,
    long ResponseTimeAsMillisecond,
    Exception? Exception,
    IRequestContent RequestContent,
    Uri? BaseAddress
);

public record ServiceClientResult<TResponse>
(
    TResponse? Response,
    string RawResponse,
    HttpStatusCode? StatusCode,
    HttpResponseHeaders ? Header,
    long ResponseTimeAsMillisecond,
    Exception? Exception,
    IRequestContent RequestContent,
    Uri? BaseAddress
) : ServiceClientResult(RawResponse, StatusCode, Header, ResponseTimeAsMillisecond, Exception, RequestContent, BaseAddress);