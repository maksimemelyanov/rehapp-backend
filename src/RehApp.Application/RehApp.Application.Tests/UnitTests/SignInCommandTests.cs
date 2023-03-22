using Moq;
using RehApp.Application.Features.Auth.Commands;
using RehApp.Infrastructure.Common.Models;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace RehApp.Application.Tests.UnitTests;

public class SignInCommandTests
{
    [Fact]
    public async Task Should_ValidationError_When_RequiredParametersAreEmpty()
    {
        // Arrange
        var signInManager = new Mock<FakeSignInManager>();
        var userManager = new Mock<FakeUserManager>();
        var request = new SignInRequest() { Login = default!, Password = default! };
        var command = new SignInCommandHandler(signInManager.Object, userManager.Object);

        // Act
        var response = await command.Handle(request, CancellationToken.None);

        // Assert
        Assert.IsType<InternalResponse>(response);
        Assert.False(response.IsSuccess);
        Assert.Equal(typeof(ValidationException), response?.Exception?.GetType());

        Assert.Contains(nameof(request.Login), response?.Exception?.Message);
        Assert.Contains(nameof(request.Password), response?.Exception?.Message);
    }
}
