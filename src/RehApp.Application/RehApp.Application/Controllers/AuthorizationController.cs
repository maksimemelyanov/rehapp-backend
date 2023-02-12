using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RehApp.Application.Features.Authorization.Commands;
using RehApp.Infrastructure.Common.Models;

namespace RehApp.Application.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Tags("Authorization")]
[Route("api/v{version:apiVersion}/auth")]
public class AuthorizationController : Controller
{
    private readonly IMediator _mediator;

    public AuthorizationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("signIn")]
    public async Task<IActionResult> SignIn(
        [FromBody] SignInRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return response.IsSuccess ? Ok() : BadRequest(new FailureResponse(response));
    }

    [HttpPost]
    [Authorize]
    [Route("signOut")]
    public async Task<IActionResult> SignOut(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new SignOutRequest(), cancellationToken);
        return response.IsSuccess ? Ok() : BadRequest(new FailureResponse(response));
    }
}
