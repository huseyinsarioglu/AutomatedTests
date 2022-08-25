namespace Automated.API.SandboxTests.Configurations;
public class AppSettings : IAppSettings
{
    public string ApiKey { get; set; } = string.Empty;

    public UrlList UrlList { get; set; } = new UrlList();
}
