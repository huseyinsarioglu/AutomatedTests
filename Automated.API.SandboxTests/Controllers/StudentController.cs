namespace Automated.API.SandboxTests.Controllers;

public record StudentControllerModel(bool SendApiKey = true);

public record StudentControllerModel<T>(T? Value, bool SendApiKey = true): StudentControllerModel(SendApiKey);

public class StudentController : ControllerBase
{
    private readonly Dictionary<string, string> _headers;

    public StudentController(HttpClient httpClient, UrlList urlList, AppSettings appSettings) : base(httpClient, urlList)
    {
        _headers = new()
        {
            { nameof(appSettings.ApiKey), appSettings.ApiKey }
        };
    }

    private Dictionary<string, string>? GetHeaders(StudentControllerModel? model) => model?.SendApiKey == true ? _headers : null;

    public Task<ServiceClientResult<Student[]>> GetAsync(StudentControllerModel? model)
    {
        return base.CallApiAsync<Student[]>(httpMethod: HttpMethod.Get, headerValues: GetHeaders(model));
    }

    public Task<ServiceClientResult<Student?>> GetByIdAsync(StudentControllerModel<int>? model)
    {
        return base.CallApiAsync<Student?>(httpMethod: HttpMethod.Get, actionUrl: $"{model?.Value}", headerValues: GetHeaders(model));
    }

    public Task<ServiceClientResult<Student?>> CreateAsync(StudentControllerModel<Student>? model)
    {
        return base.CallApiAsync<Student?, Student?>(httpMethod: HttpMethod.Post, request: model?.Value, headerValues: GetHeaders(model));
    }

    public Task<ServiceClientResult<Student?>> UpdateAsync(StudentControllerModel<Student>? model)
    {
        return base.CallApiAsync<Student?, Student?>(httpMethod: HttpMethod.Put, request: model?.Value, headerValues: GetHeaders(model));
    }

    public Task<ServiceClientResult<object?>> DeleteAsync(StudentControllerModel<int?>? model)
    {
        return base.CallApiAsync<object?>(httpMethod: HttpMethod.Delete, actionUrl: $"{model?.Value}", headerValues: GetHeaders(model));
    }
}