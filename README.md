
# Automated Tests Demo

This solution is a test automation demo. It consists of 5 projects. There are two projects for test automation, with the word Tests at the end:

* Automated.API.DummyRestapiTests
Tests APIs at https://dummy.restapiexample.com. APIs are extremely dummy and no rules or controls were found to be enforced. Not all tests are in the PASS state because API methods do not exhibit the expected behavior. It also frequently returns 429 - Too Many Attempts. This causes the states of the tests to be unstable. Still, it's useful for demonstrating my reusability ability.

* Automated.API.SandboxTests
Tests the Automated.APISandbox API project in the solution. The API project is run with WebApplicationFactory during tests, so there is no need to deploy. I developed this project as a CRUD API with simple rules. The project does not have a permanent data source, a dummy context is created and dummy records are added during each run. However, the parameter requirement and the rules of several fields in the model such as mandatory, min lenght, email are defined. 400 returns when these rules are violated. 404 is returned for records that are not in the fake database. In addition, all requests must send an API Key. Otherwise it returns 401. Successful transactions return 200, record creation returns 201. All cases are scripted and all tests are in PASS state.

Controller Object Model pattern has been applied in test projects. A controller has also been added to the test project for the actual controller to be tested. The actual controller relationship is executed from here. The test case and step definitions are abstracted from the real controller.

Hook samples have also been added to increase reusability and performance.

Dependency Injection (DI) was used in test projects. Two test projects are configured with appsettings.json.

Helper Projects:
* Automated.APISandbox
It is the SUT for the SandboxTests project. It is a simple CRUD API with simple rules as mentioned in SandboxTests.

* HttpClientHelper.Library
As the name suggests, it performs http requests of API tests. Automatically converts models returned and sent to API. Various information is returned about the http request made.

* Automated.API.Common
It contains the base objects of API test projects and their helper methods.

Includes StepDefinitionBase class to increase reusability. Also, this class partially contains common GWT methods. For example, both test projects have no Given and Then methods internally. There are only WHEN methods specific to their domains.

No setup or deployment is required. Just .NET Core 6 and Visual Studio are enough. After compiling the Solution, they should appear in the Test Explorer.

I really paid attention to. I hope you will like it.

