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
        //Arrange
        var assembly = typeof(Domain.Models.Product).Assembly;

        //Act
        var wepApiDependencyResult = Types.InAssembly(assembly)
            .Should()
            .NotHaveDependencyOn(WepApiNamespace)
            .GetResult();

        var infrastructureDependencyResult = Types.InAssembly(assembly)
            .Should()
            .NotHaveDependencyOn(InfrastructureNamespace)
            .GetResult();
        
        var applicationDependencyResult = Types.InAssembly(assembly)
            .Should()
            .NotHaveDependencyOn(ApplicationNamespace)
            .GetResult();

        //Assert
        wepApiDependencyResult.IsSuccessful.Should().BeTrue();
        infrastructureDependencyResult.IsSuccessful.Should().BeTrue();
        applicationDependencyResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Application_Should_Not_HaveDependencyOnOtherProjects()
    {
        //Arrange
        var assembly = typeof(Application.Extensions.DependencyRegistrationExtention).Assembly;

        //Act
        var wepApiDependencyResult = Types.InAssembly(assembly)
            .Should()
            .NotHaveDependencyOn(WepApiNamespace)
            .GetResult();

        var infrastructureDependencyResult = Types.InAssembly(assembly)
            .Should()
            .NotHaveDependencyOn(InfrastructureNamespace)
            .GetResult();

        //Assert
        wepApiDependencyResult.IsSuccessful.Should().BeTrue();
        infrastructureDependencyResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Infrastructure_Should_Not_HaveDependencyOnOtherProjects()
    {
        //Arrange
        var assembly = typeof(Infrastructure.Extensions.DependencyRegistrationExtention).Assembly;

        //Act
        var wepApiDependencyResult = Types.InAssembly(assembly)
            .Should()
            .NotHaveDependencyOn(WepApiNamespace)
            .GetResult();

        //Assert
        wepApiDependencyResult.IsSuccessful.Should().BeTrue();
    }
}