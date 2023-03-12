using MediatR;
using Microsoft.AspNetCore.Identity;
using RehApp.Domain.RelationalDatabase.Entities;
using RehApp.Domain.RelationalDatabase.Enums;
using RehApp.Infrastructure.Common.Exceptions;
using RehApp.Infrastructure.Common.Extensions;
using RehApp.Infrastructure.Common.Models;
using System.Security.Claims;

namespace RehApp.Application.Features.User.Commands;

public class CreateUserByExternalLoginInfoRequest : IRequest<InternalResponse>
{

}

public class CreateUserByExternalLoginInfoCommandHandler
    : IRequestHandler<CreateUserByExternalLoginInfoRequest, InternalResponse>
{
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly UserManager<ApplicationUser> userManager;

    public CreateUserByExternalLoginInfoCommandHandler(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
    }

    public async Task<InternalResponse> Handle(
        CreateUserByExternalLoginInfoRequest request,
        CancellationToken cancellationToken)
    {
        var response = new InternalResponse();

        var info = await signInManager.GetExternalLoginInfoAsync();
        if (info is null) return response.Failure(Exceptions.FailedToGetUserData);

        var provider = Enum.Parse<AuthProvider>(info.LoginProvider);
        var externalId = info.Principal.Claims.GetByClaimType(ClaimTypes.NameIdentifier);
        var firstName = info.Principal.Claims.GetByClaimType(ClaimTypes.GivenName);
        var surname = info.Principal.Claims.GetByClaimType(ClaimTypes.Surname);

        if (new[] { externalId, firstName, surname }.Any(string.IsNullOrEmpty))
        {
            return response.Failure(Exceptions.FailedToGetUserData);
        }

        var username = $"{info.LoginProvider}-{externalId}";
        var email = info.Principal.Claims.GetByClaimType(ClaimTypes.Email);

        var user = new ApplicationUser { UserName = username, Email = email };

        var identityResult = await userManager.CreateAsync(user);
        if (!identityResult.Succeeded)
        {
            var message = string.Join("; ", identityResult.Errors);
            return response.Failure(new InvalidOperationException(message));
        }

        await userManager.AddClaimAsync(user, new Claim("Id", user.Id.ToString()));

        return response.Success();
    }
}