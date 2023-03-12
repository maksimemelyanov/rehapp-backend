using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RehApp.Application.Features.Authorization.Commands;
using RehApp.Application.Features.Token.Commands;
using RehApp.Application.Features.User.Commands;
using RehApp.Application.Features.User.Queries;
using RehApp.Infrastructure.Common.Enums;
using RehApp.Infrastructure.Common.Models;

namespace RehApp.Application.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Tags("Security")]
[Route("api/v{version:apiVersion}/security")]
public class AuthorizationController : Controller
{
    private readonly IMediator mediator;

    public AuthorizationController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    /// <summary>
    /// Cookie-based authentication
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("cookie/signIn")]
    public async Task<IActionResult> SignIn(
        [FromBody] SignInRequest request,
        CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);
        return response.IsSuccess ? Ok() : BadRequest(new FailureResponse(response));
    }

    /// <summary>
    /// Cookie-based authentication via an external provider. 
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
    [HttpGet("cookie/signIn/{scheme}")]
    public async Task<IActionResult> SignInViaCookieUsingExternalProvider(
        [FromRoute] string scheme,
        [FromQuery] string callback,
        CancellationToken cancellationToken)
    {
        return await SignInUsingExternalProvider(scheme, callback, AuthMethod.Cookie, cancellationToken);
    }

    /// <summary>
    /// Sign out of the current user's account
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize]
    [Route("cookie/signOut")]
    public async Task<IActionResult> SignOut(CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new SignOutRequest(), cancellationToken);
        return response.IsSuccess ? Ok() : BadRequest(new FailureResponse(response));
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

        var existRequest = new CheckUserExistenceByExternalLoginInfoRequest();
        var existResponse = await mediator.Send(existRequest, cancellationToken);
        if (!existResponse.IsSuccess) return BadRequest(new FailureResponse(existResponse));

        if (!existResponse.Data)
        {
            var createUserRequest = new CreateUserByExternalLoginInfoRequest();
            var createUserResponse = await mediator.Send(createUserRequest, cancellationToken);
            if (!createUserResponse.IsSuccess) return BadRequest(new FailureResponse(createUserResponse));
        }

        var request = new SignInUsingExternalProviderRequest() { Callback = callback, AuthMethod = authMethod };
        var response = await mediator.Send(request, cancellationToken);
        if (!response.IsSuccess) return BadRequest(new FailureResponse(response));

        return Redirect(response.Data!);
    }
}
