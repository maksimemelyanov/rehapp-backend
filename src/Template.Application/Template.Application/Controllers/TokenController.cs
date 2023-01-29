using MediatR;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Features.Token.Commands;
using Template.Infrastructure.Common.Models;

namespace Template.Application.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Tags("Token")]
[Route("api/v{version:apiVersion}/token")]
public class TokenController : Controller
{
    private readonly IMediator _mediator;

    public TokenController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateToken(
        [FromBody] CreateTokenRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return response.IsSuccess ? Ok(response.Data) : BadRequest(new FailureResponse(response));
    }

    [HttpPut]
    public async Task<IActionResult> RefreshToken(
        [FromBody] RefreshTokenRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return response.IsSuccess ? Ok(response.Data) : BadRequest(new FailureResponse(response));
    }
}
