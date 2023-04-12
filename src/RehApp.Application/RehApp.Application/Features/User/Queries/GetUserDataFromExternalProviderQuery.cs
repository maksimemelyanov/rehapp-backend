using MediatR;
using Microsoft.AspNetCore.Identity;
using RehApp.Application.DTOs;
using RehApp.Domain.RelationalDatabase.Entities;
using RehApp.Domain.RelationalDatabase.Enums;
using RehApp.Infrastructure.Common.Exceptions;
using RehApp.Infrastructure.Common.Extensions;
using RehApp.Infrastructure.Common.Models;
using System.Security.Claims;

namespace RehApp.Application.Features.User.Queries;

public class GetUserDataFromExternalProviderRequest : IRequest<InternalResponse<ExternalUserDTO>> { }

public class GetUserDataFromExternalProviderQueryHandler
    : IRequestHandler<GetUserDataFromExternalProviderRequest, InternalResponse<ExternalUserDTO>>
{
    private readonly SignInManager<ApplicationUser> signInManager;

    public GetUserDataFromExternalProviderQueryHandler(
        SignInManager<ApplicationUser> signInManager)
    {
        this.signInManager = signInManager;
    }

    public async Task<InternalResponse<ExternalUserDTO>> Handle(
        GetUserDataFromExternalProviderRequest request,
        CancellationToken cancellationToken)
    {
        var response = new InternalResponse<ExternalUserDTO>();

        var info = await signInManager.GetExternalLoginInfoAsync();
        if (info is null) return response.Failure(Exceptions.FailedToGetUserData);

        var extId = info.Principal.Claims.GetByClaimType(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(extId) || !Enum.TryParse<AuthProvider>(info.LoginProvider, out var prvdr))
        {
            return response.Failure(Exceptions.FailedToGetUserData);
        }

        var user = new ExternalUserDTO
        {
            FirstName = info.Principal.Claims.GetByClaimType(ClaimTypes.GivenName),
            Surname = info.Principal.Claims.GetByClaimType(ClaimTypes.Surname),
            Username = $"{prvdr}-{extId}",
            Email = info.Principal.Claims.GetByClaimType(ClaimTypes.Email)
        };

        return response.Success(user);
    }
}
