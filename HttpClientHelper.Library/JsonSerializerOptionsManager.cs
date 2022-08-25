namespace HttpClientHelper.Library;
public static class JsonSerializerOptionsManager
{
    private static readonly Lazy<JsonSerializerOptions> _settings = new Lazy<JsonSerializerOptions>(CreateSerializerSettings);

    private static JsonSerializerOptions CreateSerializerSettings()
    {
        return new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
    }

    public static JsonSerializerOptions Options => _settings.Value;
}
