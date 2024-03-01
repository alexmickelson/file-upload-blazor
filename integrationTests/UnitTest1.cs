namespace integrationTests;

using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

// https://github.com/dotnet/AspNetCore.Docs.Samples/tree/main/test/integration-tests/8.x/IntegrationTestsSample
public class UnitTest1 : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task Test1()
    {
        Environment.SetEnvironmentVariable("MYDATABASECONNECTIONSTRING", "Server=alex-cool-integration-db;password=mypassword;user id=alex;database=alexdb;");
        var factory = new WebApplicationFactory<Program>();
        var httpclient = factory.CreateClient();
        // simulate a get request


        var defaultPage = await httpclient.GetAsync("/");

        defaultPage.StatusCode.Should().Be(HttpStatusCode.OK);
        
    }
}