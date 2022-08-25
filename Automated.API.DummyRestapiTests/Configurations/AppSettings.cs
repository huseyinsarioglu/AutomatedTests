using Automated.API.Common.Interfaces;

namespace Automated.API.DummyRestapiTests.Configurations;
public class AppSettings : IAppSettings
{
    public UrlList UrlList { get; set; } = new UrlList();
}
