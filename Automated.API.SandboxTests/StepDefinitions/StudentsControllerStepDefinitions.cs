namespace Automated.API.SandboxTests.StepDefinitions;

public class StudentsControllerStepDefinitions : StepDefinitionBase
{
    private readonly StudentController _studentController;

    public StudentsControllerStepDefinitions(StudentController studentController, TestContext testContext, ScenarioContext scenarioContext, FeatureContext featureContext) : base(testContext, scenarioContext, featureContext)
    {
        _studentController = studentController;
    }

    [When(@"I call the get all students action")]
    public Task WhenICallTheGetAllStudentsAction()
    {
        return CallApiAndKeepResponseInScenarioContextAsync<StudentControllerModel?, Student[]>(_studentController.GetAsync);
    }

    [When(@"I call the get by Id student action")]
    public Task WhenICallTheGetByIdStudentAction()
    {
        return CallApiAndKeepResponseInScenarioContextAsync<StudentControllerModel<int>?, Student?>(_studentController.GetByIdAsync);
    }

    [When(@"I call the create student action")]
    public Task WhenICallTheCreateStudentAction()
    {
        return CallApiAndKeepResponseInScenarioContextAsync<StudentControllerModel<Student>?, Student?>(_studentController.CreateAsync);
    }

    [When(@"I call the update student action")]
    public Task WhenICallTheUpdateStudentAction()
    {
        return CallApiAndKeepResponseInScenarioContextAsync<StudentControllerModel<Student>?, Student?>(_studentController.UpdateAsync);
    }

    [When(@"I call the delete student action")]
    public Task WhenICallTheDeleteStudentAction()
    {
        return CallApiAndKeepResponseInScenarioContextAsync<StudentControllerModel<int?>?, object?>(_studentController.DeleteAsync);
    }
}
