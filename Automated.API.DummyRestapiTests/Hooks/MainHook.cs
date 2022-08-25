using BoDi;

namespace Automated.API.DummyRestapiTests.Hooks;

[Binding]
public sealed class MainHook
{
    [BeforeTestRun(Order = 1)]
    public static void BeforeTest(IObjectContainer container)
    {
        var configuration = new ConfigurationBase<AppSettings>();
        container.RegisterInstanceAs(configuration.AppSettings);
    }
}