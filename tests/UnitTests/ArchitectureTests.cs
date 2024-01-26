using FluentAssertions;
using NetArchTest.Rules;
using Xunit;

namespace UnitTests;

public class ArchitectureTests
{
    private const string ApplicationNamespace = "Application";
    private const string InfrastructureNamespace = "Infrastructure";
    private const string WepApiNamespace = "WebApi";

    [Fact]
    public void Domain_Should_Not_HaveDependencyOnOtherProjects()
    {
        var assembly = typeof(Domain.Models.Product).Assembly;

        var otherProjects = new[]
        {
            ApplicationNamespace,
            InfrastructureNamespace,
            WepApiNamespace
        };

        var testResult = Types.InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Application_Should_Not_HaveDependencyOnOtherProjects()
    {
        var assembly = typeof(Application.Extensions.DependencyRegistrationExtention).Assembly;

        var otherProjects = new[]
        {
            InfrastructureNamespace,
            WepApiNamespace
        };

        var testResult = Types.InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Infrastructure_Should_Not_HaveDependencyOnOtherProjects()
    {
        var assembly = typeof(Infrastructure.Extensions.DependencyRegistrationExtention).Assembly;

        var otherProjects = new[]
        {
            WepApiNamespace
        };

        var testResult = Types.InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        testResult.IsSuccessful.Should().BeTrue();
    }
}