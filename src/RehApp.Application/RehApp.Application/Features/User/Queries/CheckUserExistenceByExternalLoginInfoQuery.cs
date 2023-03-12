using MediatR;
using Microsoft.AspNetCore.Identity;
using RehApp.Application.Extensions;
using RehApp.Domain.RelationalDatabase.Entities;
using RehApp.Domain.RelationalDatabase.Enums;
using RehApp.Infrastructure.Common.Exceptions;
using RehApp.Infrastructure.Common.Extensions;
using RehApp.Infrastructure.Common.Models;
using System.Security.Claims;

namespace RehApp.Application.Features.User.Queries;

public class CheckUserExistenceByExternalLoginInfoRequest : IRequest<InternalResponse<bool>> { }

public class CheckUserExistenceByExternalLoginInfoQueryHandler
    : IRequestHandler<CheckUserExistenceByExternalLoginInfoRequest, InternalResponse<bool>>
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly SignInManager<ApplicationUser> signInManager;

    public CheckUserExistenceByExternalLoginInfoQueryHandler(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
    }

    public async Task<InternalResponse<bool>> Handle(
        CheckUserExistenceByExternalLoginInfoRequest request,
        CancellationToken cancellationToken)
    {
        var response = new InternalResponse<bool>();

        var info = await signInManager.GetExternalLoginInfoAsync();
        if (info is null) return response.Failure(Exceptions.FailedToGetUserData);

        var provider = Enum.Parse<AuthProvider>(info!.LoginProvider);
        var externalId = info.Principal.Claims.GetByClaimType(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(externalId)) return response.Failure(Exceptions.FailedToGetUserData);

        var user = await userManager.FindByExternalLoginInfoAsync(provider, externalId!);
        return response.Success(user is not null);
    }
}
