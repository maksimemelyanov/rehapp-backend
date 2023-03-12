using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RehApp.Domain.RelationalDatabase.Entities;
using RehApp.Infrastructure.Common;
using RehApp.Infrastructure.Common.Exceptions;
using RehApp.Infrastructure.Common.Extensions;
using RehApp.Infrastructure.Common.Helpers;
using RehApp.Infrastructure.Common.Models;
using System.ComponentModel.DataAnnotations;
using Models = RehApp.Infrastructure.Common.Models;

namespace RehApp.Application.Features.Token.Commands;

public class RefreshTokenRequest : IRequest<InternalResponse<Models.Token>>
{
    [Required]
    public string ExpiredAccessToken { get; set; } = null!;

    [Required]
    public string RefreshToken { get; set; } = null!;
}

public class RefreshTokenCommandHandler : 
    IRequestHandler<RefreshTokenRequest, InternalResponse<Models.Token>>
{
    private readonly TokenValidationParameters validationParameters;
    private readonly BearerSettings bearerSettings;
    private readonly UserManager<ApplicationUser> userManager;

    public RefreshTokenCommandHandler(
        UserManager<ApplicationUser> userManager,
        BearerSettings bearerSettings,
        TokenValidationParameters validationParameters)
    {
        this.bearerSettings = bearerSettings;
        this.validationParameters = validationParameters;
        this.userManager = userManager;
    }

    public async Task<InternalResponse<Models.Token>> Handle(
        RefreshTokenRequest request, 
        CancellationToken cancellationToken)
    {
        var response = new InternalResponse<Models.Token>();

        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return response.Failure(validation);

        var principal = TokenHelper.GetPrincipalFromExpiredToken(
            accessToken: request.ExpiredAccessToken,
            validationParameters: validationParameters);

        if (principal is null) return response.Failure(Exceptions.InvalidRefreshTokenData);

        if (!Guid.TryParse(principal!.FindFirst("Id")?.Value, out var id))
        {
            return response.Failure(Exceptions.FailedToIdentifyUser);
        }

        var user = await userManager.FindByIdAsync(id.ToString());
        if (user is null) return response.Failure(Exceptions.FailedToIdentifyUser);

        if (user.RefreshToken != request.RefreshToken)
        {
            return response.Failure(Exceptions.InvalidRefreshTokenData);
        }

        if (user.RefreshTokenExpiryTime < DateTime.UtcNow)
        {
            return response.Failure(Exceptions.RefreshTokenExpired);
        }

        var newAccessToken = TokenHelper.GenerateAccessToken(
            key: bearerSettings.Key,
            issuer: bearerSettings.Issuer,
            audience: bearerSettings.Audience,
            expires: DateTime.UtcNow.AddSeconds(bearerSettings.AccessTokenLifetime),
            claims: principal!.Claims.ToList());

        return response.Success(new Models.Token
        {
            AccessToken = newAccessToken,
            RefreshToken = user.RefreshToken,
        });
    }
}
