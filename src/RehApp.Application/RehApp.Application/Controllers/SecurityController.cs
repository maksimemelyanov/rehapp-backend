using MediatR;
using Microsoft.AspNetCore.Mvc;
using RehApp.Application.Features.Auth.Commands;
using RehApp.Application.Features.Token.Commands;
using RehApp.Application.Features.User.Queries;
using RehApp.Infrastructure.Common.Enums;
using RehApp.Infrastructure.Common.Models;

namespace RehApp.Application.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Tags("Security")]
[Route("api/v{version:apiVersion}/security")]
public class SecurityController : Controller
{
    private readonly IMediator mediator;

    public SecurityController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    /// <summary>
    /// Token-based authentication
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("token")]
    public async Task<IActionResult> CreateToken(
        [FromBody] CreateTokenRequest request,
        CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);
        return response.IsSuccess ? Ok(response.Data) : BadRequest(new FailureResponse(response));
    }

    /// <summary>
    /// Update expired 'accessToken' with 'refreshToken'
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("token")]
    public async Task<IActionResult> RefreshToken(
        [FromBody] RefreshTokenRequest request,
        CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);
        return response.IsSuccess ? Ok(response.Data) : BadRequest(new FailureResponse(response));
    }

    /// <summary>
    /// Token-based authentication via an external provider. 
    /// WARNING: Calling this method does not work via swagger
    /// </summary>
    /// <param name="scheme">
    /// The case-sensitive name of the external provider. Currently available:
    /// <ul><li>Yandex</li><li>Vkontakte</li></ul>
    /// </param>
    /// <param name="callback">
    /// The absolute path to which redirection will be made in case of successful login to the account.
    /// </param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("token/{scheme}")]
    public async Task<IActionResult> SignInViaTokenUsingExternalProvider(
        [FromRoute] string scheme,
        [FromQuery] string callback,
        CancellationToken cancellationToken)
    {
        return await SignInUsingExternalProvider(scheme, callback, AuthMethod.Token, cancellationToken);
    }

    private async Task<IActionResult> SignInUsingExternalProvider(
        string scheme,
        string callback,
        AuthMethod authMethod,
        CancellationToken cancellationToken)
    {
        var request = new ConfigureAuthPropertiesRequest(scheme, authMethod, callback);
        var response = await mediator.Send(request, cancellationToken);
        if (!response.IsSuccess) return BadRequest(response);
        return Challenge(response.Data!, scheme);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [Route($"/{ExternalSignInHandlerRelativePath}")]
    public async Task<IActionResult> ExternalAuthHandler(
        [FromQuery] string remoteError,
        [FromQuery] string callback,
        [FromQuery] AuthMethod authMethod,
        CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(remoteError)) return BadRequest(new FailureResponse(remoteError));

        var externalUserDataRequest = new GetUserDataFromExternalProviderRequest();
        var externalUser = await mediator.Send(externalUserDataRequest, cancellationToken);
        if (!externalUser.IsSuccess) return BadRequest(new FailureResponse(externalUser));

        var existRequest = new CheckUserExistByLoginRequest() { Login = externalUser.Data!.Username };
        var existResponse = await mediator.Send(existRequest, cancellationToken);
        if (!existResponse.IsSuccess) return BadRequest(new FailureResponse(existResponse));
        if (!existResponse.Data)
        {
            var message = "User registration required";
            return BadRequest(new FailureResponse(message, externalUser.Data));
        }

        var request = new SignInUsingExternalProviderRequest() { Callback = callback, AuthMethod = authMethod };
        var response = await mediator.Send(request, cancellationToken);
        if (!response.IsSuccess) return BadRequest(new FailureResponse(response));

        return Redirect(response.Data!);
    }
}
