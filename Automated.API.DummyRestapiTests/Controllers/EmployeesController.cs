namespace Automated.API.DummyRestapiTests.Controllers;

public class EmployeesController : ControllerBase
{
    public record PostData(
        int Id,
        string Name,
        int Age,
        decimal Salary
    );

    public record Data(
        int Id,
        
        [property:JsonPropertyName("employee_name")]
        string Name,

        [property:JsonPropertyName("employee_age")]
        int Age,

        [property:JsonPropertyName("employee_salary")]
        decimal Salary,

        [property:JsonPropertyName("profile_image")]
        string ProfileImage
    );

    public record Employees<T>(string Status, T Data, string Message);


    public EmployeesController(AppSettings appSettings): base(appSettings.UrlList)
	{
	}

    public Task<ServiceClientResult<Employees<Data[]>>> GetAsync()
    {
        return base.GetApiAsync<Employees<Data[]>>("employees");
    }

    public Task<ServiceClientResult<Employees<Data>>> GetAsync(int? id)
    {
        return base.GetApiAsync<Employees<Data>>($"employee/{id}");
    }

    public Task<ServiceClientResult<Employees<Data>>> CreateAsync(PostData? data)
    {
        return base.CallApiAsync<PostData, Employees<Data>>(HttpMethod.Post, "create", data);
    }

    public Task<ServiceClientResult<Employees<Data>>> UpdateAsync(PostData? data)
    {
        return base.CallApiAsync<PostData, Employees<Data>>(HttpMethod.Put, $"update/{data?.Id}", data);
    }

    public Task<ServiceClientResult<Employees<Data>>> DeleteAsync(int? id)
    {
        return base.CallApiAsync<Employees<Data>>(HttpMethod.Delete, $"delete/{id}");
    }
}