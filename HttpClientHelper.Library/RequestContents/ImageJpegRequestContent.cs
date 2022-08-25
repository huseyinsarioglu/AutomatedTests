namespace HttpClientHelper.Library.RequestContents;

public class ImageJpegRequestContent : RequestContentBase<FileStream?>, IDisposable
{
    private MultipartFormDataContent? _multipartFormContent;
    private StreamContent? _fileStreamContent;

    //public ImageJpegRequestContent(FileStream stream, string url, HttpMethod httpMethod) : base(stream, url, httpMethod)
    public ImageJpegRequestContent(RequestContentCtor<FileStream?> ctor) : base(ctor)
    {
    }

    //substring(1) => skip period at first
    public override string ContentType => $"image/{Path.GetExtension(_request?.Name)?.Substring(1)}";

    protected override void SetRequestContentJson()
    {
        RequestContentJson = $"byte[{_request?.Length}]";
    }

    protected override void ProcessRequestContent(HttpClient httpClient, HttpRequestMessage httpRequest)
    {
        ArgumentNullException.ThrowIfNull(_request);
        _multipartFormContent?.Dispose();
        _multipartFormContent = new MultipartFormDataContent();

        _fileStreamContent = new StreamContent(_request);
        _fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue(ContentType);

        _multipartFormContent.Add(_fileStreamContent, name: "file", fileName: Path.GetFileName(_request.Name));
        httpRequest.Content = _multipartFormContent;
    }

    public void Dispose()
    {
        _multipartFormContent?.Dispose();
        _fileStreamContent?.Dispose();
        GC.SuppressFinalize(this);
    }
}