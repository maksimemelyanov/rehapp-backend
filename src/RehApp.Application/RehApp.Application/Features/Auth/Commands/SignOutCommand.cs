using MediatR;
using Microsoft.AspNetCore.Identity;
using RehApp.Domain.RelationalDatabase.Entities;
using RehApp.Infrastructure.Common.Models;

namespace RehApp.Application.Features.Auth.Commands;

public class SignOutRequest : IRequest<InternalResponse>
{

}

public class SignOutCommandHandler : IRequestHandler<SignOutRequest, InternalResponse>
{
    private readonly SignInManager<ApplicationUser> signInManager;

    public SignOutCommandHandler(SignInManager<ApplicationUser> signInManager)
    {
        this.signInManager = signInManager;
    }

    public async Task<InternalResponse> Handle(
        SignOutRequest request, 
        CancellationToken cancellationToken)
    {
        var response = new InternalResponse();

        await signInManager.SignOutAsync();

        return response.Success();
    }
}
