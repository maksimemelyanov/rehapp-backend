using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RehApp.Application.Features.Account.Commands;
using RehApp.Infrastructure.Common.Models;

namespace RehApp.Application.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Tags("Account")]
[Route("api/v{version:apiVersion}/account")]
public class AccountController : Controller
{
    private readonly IMediator mediator;

    public AccountController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    /// <summary>
    /// Send an email to the user for password recovery
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    [Route("reset-password/send-mail")]
    public async Task<IActionResult> ResetPassword(
        [FromBody] SendPasswordResetMailRequest request,
        CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);
        return response.IsSuccess ? Ok(response) : BadRequest(new FailureResponse(response));
    }

    /// <summary>
    /// Update the user's lost password
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    [Route("reset-password")]
    public async Task<IActionResult> ResetPasswordHandler(
        [FromBody] SetPasswordRequest request,
        CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);
        return response.IsSuccess ? Ok(response) : BadRequest(new FailureResponse(response));
    }
}
