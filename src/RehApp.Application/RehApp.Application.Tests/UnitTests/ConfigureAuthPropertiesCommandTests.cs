using Microsoft.AspNetCore.Authentication;
using Moq;
using RehApp.Application.Features.Auth.Commands;
using RehApp.Domain.RelationalDatabase.Enums;
using RehApp.Infrastructure.Common.Enums;
using RehApp.Infrastructure.Common.Models;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace RehApp.Application.Tests.UnitTests;

public class ConfigureAuthPropertiesCommandTests
{
    [Fact]
    public async Task Should_ValidationError_When_RequiredParametersAreEmpty()
    {
        // Arrange
        var signInManager = new Mock<FakeSignInManager>();
        var request = new ConfigureAuthPropertiesRequest(default!, default, default!);
        var command = new ConfigureAuthPropertiesCommandHandler(signInManager.Object);

        // Act
        var response = await command.Handle(request, CancellationToken.None);

        // Assert
        Assert.IsType<InternalResponse<AuthenticationProperties>>(response);
        Assert.False(response.IsSuccess);
        Assert.Equal(typeof(ValidationException), response?.Exception?.GetType());

        Assert.Contains(nameof(request.Provider), response?.Exception?.Message);
        Assert.Contains(nameof(request.Callback), response?.Exception?.Message);
        Assert.DoesNotContain(nameof(request.AuthMethod), response?.Exception?.Message);
    }

    [Fact]
    public async Task Should_ValidationError_When_PassedProviderIsNotPartOfAuthProviderEnum()
    {
        // Arrange
        var signInManager = new Mock<FakeSignInManager>();
        var request = new ConfigureAuthPropertiesRequest("Facebook", AuthMethod.Cookie, "some callback");
        var command = new ConfigureAuthPropertiesCommandHandler(signInManager.Object);

        // Act
        var response = await command.Handle(request, CancellationToken.None);

        // Assert
        Assert.IsType<InternalResponse<AuthenticationProperties>>(response);
        Assert.False(response.IsSuccess);
        Assert.Equal(typeof(ValidationException), response?.Exception?.GetType());

        Assert.DoesNotContain(nameof(request.AuthMethod), response?.Exception?.Message);
        Assert.DoesNotContain(nameof(request.Callback), response?.Exception?.Message);
        Assert.Contains(nameof(AuthProvider), response?.Exception?.Message);
    }
}
