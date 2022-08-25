using Automated.API.Common.Base;
using Automated.API.DummyRestapiTests.Controllers;
using System.Net;
using static Automated.API.DummyRestapiTests.Controllers.EmployeesController;

namespace Automated.API.DummyRestapiTests.StepDefinitions;

public class EmployeesControllerStepDefinitions : StepDefinitionBase
{
    private readonly EmployeesController _employeesController;

    public EmployeesControllerStepDefinitions(EmployeesController employeesController, TestContext testContext, ScenarioContext scenarioContext, FeatureContext featureContext) : base(testContext, scenarioContext, featureContext)
    {
        _employeesController = employeesController;
    }

    [When(@"I call the get all employees action")]
    public Task WhenICallTheGetAllEmployeesAPI()
    {
        return CallApiAndKeepResponseInScenarioContextAsync(_employeesController.GetAsync);
    }

    [When(@"I call the get employee by id action")]
    public Task WhenICallTheGetEmployeeByIdAction()
    {
        return CallApiAndKeepResponseInScenarioContextAsync<int?, Employees<Data>>(_employeesController.GetAsync);
    }

    [When(@"I call the Create employee action")]
    public Task WhenICallTheCreateEmployeeAction()
    {
        return CallApiAndKeepResponseInScenarioContextAsync<PostData?, Employees<Data>>(_employeesController.CreateAsync);
    }

    [When(@"I call the Update employee action")]
    public Task WhenICallTheUpdateEmployeeAction()
    {
        return CallApiAndKeepResponseInScenarioContextAsync<PostData?, Employees<Data>>(_employeesController.UpdateAsync);
    }

    [When(@"I call the delete employee by id action")]
    public Task WhenICallTheDeleteEmployeeByIdAction()
    {
        return CallApiAndKeepResponseInScenarioContextAsync<int?, Employees<Data>>(_employeesController.DeleteAsync);
    }
}
