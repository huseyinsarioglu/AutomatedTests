using HttpClientHelper.Library.RequestContents.Base;

namespace HttpClientHelper.Library.RequestContents;

public static class RequestContentFactory
{
    public static IRequestContent CreateRequestContent<T>(T? request, HttpMethod httpMethod, string url, bool isRequestParameterFromUri = false, Dictionary<string, string>? headerValues = null)
    {
        Type contentType = typeof(JsonRequestContent<T>);
        if (request is Stream)
        {
            contentType = typeof(ImageJpegRequestContent);
        }
        else if (isRequestParameterFromUri)
        {
            contentType = typeof(UrlBasedRequestContent<T>);
        }

        var ctor = new RequestContentCtor<T>(request, url, httpMethod, headerValues);

        var requestContent = Activator.CreateInstance(contentType, ctor) as IRequestContent;
        ArgumentNullException.ThrowIfNull(requestContent);
        return requestContent;
    }

    public static IRequestContent CreateRequestContent(HttpMethod httpMethod, string url, bool isRequestParameterFromUri = false, Dictionary<string, string>? headerValues = null)
    {
        return CreateRequestContent<object>(null, httpMethod, url, isRequestParameterFromUri, headerValues);
    }
}
