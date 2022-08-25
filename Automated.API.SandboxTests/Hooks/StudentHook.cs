namespace Automated.API.SandboxTests.Hooks;

[Binding]
public class StudentHook
{
    [BeforeTestRun(Order = 1)]
    public static void BeforeTest(IObjectContainer container, ConfigurationBase<AppSettings> configuration)
    {
        container.RegisterInstanceAs(configuration.AppSettings, configuration.AppSettings.GetType());
    }

    private const string StudentsControllerTag = "StudentsController";
    [BeforeFeature(StudentsControllerTag, Order = 1)]
    public static void BeforeFeature(IObjectContainer container)
    {
        var apiApplication = new WebApplicationFactory<Program>();
        var httpClient = apiApplication.CreateClient();
        container.RegisterInstanceAs(apiApplication, apiApplication.GetType());
        container.RegisterInstanceAs(httpClient, httpClient.GetType());
    }

    [AfterFeature(StudentsControllerTag)]
    public static void AfterTest(WebApplicationFactory<Program> apiApplication)
    {
        apiApplication?.Dispose();
    }
}
