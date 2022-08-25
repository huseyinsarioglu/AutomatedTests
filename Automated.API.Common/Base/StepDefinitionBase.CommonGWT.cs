namespace Automated.API.Common.Base;
public partial class StepDefinitionBase
{
    [Given(@"model '([^']*)'")]
    [Given(@"'([^']*)' model")]
    public void GivenModel(string model)
    {
        AddRequestToScenarioContext(model);
    }

    [Then("it should return (.*)")]
    public void ThenItShouldReturnStatusCode(HttpStatusCode statusCode)
    {
        var serviceClientResult = GetResultFromScenarioContext();
        serviceClientResult?.StatusCode.Should().Be(statusCode);
    }

    [Then(@"raw response message should contain ""([^""]*)""")]
    [Then(@"raw response message should contain '([^']*)'")]
    public void ThenRawResponseMessageShouldContain(string expected)
    {
        var serviceClientResult = GetResultFromScenarioContext();
        
        var actual = serviceClientResult?.RawResponse ?? string.Empty;
        if (!expected.Equals(actual))
        {
            actual.Should().Contain(expected);
        }
    }
}