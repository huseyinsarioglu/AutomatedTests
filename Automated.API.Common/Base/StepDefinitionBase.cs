using System.Text.Json;

namespace Automated.API.Common.Base;

[Binding]
public abstract partial class StepDefinitionBase
{
    protected const string Result = "result";
    protected const string Request = "request";
    protected const string EmptyJsonObject = "{}";

    protected readonly TestContext _testContext;
    private readonly ScenarioContext _scenarioContext;
    private readonly FeatureContext _featureContext;

    public StepDefinitionBase(TestContext testContext, ScenarioContext scenarioContext, FeatureContext featureContext)
    {
        _testContext = testContext;
        _scenarioContext = scenarioContext;
        _featureContext = featureContext;
    }

    protected string GetRequetFromScenarioContext() => GetFromContext<string>(Request, _scenarioContext) ?? string.Empty;

    protected void AddRequestToScenarioContext<T>(T value) => _scenarioContext[Request] = GetRequestAsString(value);

    private string GetRequestAsString<T>(T value)
    {
        return typeof(T) == typeof(string)
             ? value?.ToString() ?? EmptyJsonObject
             : JsonSerializer.Serialize(value, JsonSerializerOptionsManager.Options);
    }

    protected void AddResultToScenarioContext<T>(T value) => _scenarioContext[Result] = value;

    private static T? GetFromContext<T>(string key, SpecFlowContext context) => context.ContainsKey(key) ? context.Get<T>(key) : default;

    protected T? GetFromFeatureContext<T>(string key) => GetFromContext<T>(key, _featureContext);

    protected ServiceClientResult? GetResultFromScenarioContext() => GetFromContext<ServiceClientResult>(Result, _scenarioContext);

    protected async Task CallApiAndKeepResponseInScenarioContextAsync<T>(Func<Task<ServiceClientResult<T>>> apiCall)
    {
        var response = await apiCall();
        AddResultToScenarioContext(response);
        AfterCallApi(response);
    }

    protected async Task CallApiAndKeepResponseInScenarioContextAsync<TReq, TRes>(Func<TReq?, Task<ServiceClientResult<TRes>>> apiCall)
    {
        var requestString = GetRequetFromScenarioContext();
        TReq? request = string.IsNullOrEmpty(requestString) || requestString.Equals(EmptyJsonObject)
                      ? default
                      : JsonSerializer.Deserialize<TReq?>(requestString, JsonSerializerOptionsManager.Options);

        var response = await apiCall(request);
        AfterCallApi(response);
    }

    private void AfterCallApi<TRes>(ServiceClientResult<TRes> response)
    {
        var requestContent = response.RequestContent;
        _testContext.WriteLine(@$"Service Client
    Url: {response.BaseAddress}{requestContent.RequestContentCtor.Url} ({requestContent.RequestContentCtor.HttpMethod})
    Response Time: {response.ResponseTimeAsMillisecond} ms
    Request: {requestContent.RequestContentJson}
    Response: {response.RawResponse}");

        AddResultToScenarioContext(response);
    }
}