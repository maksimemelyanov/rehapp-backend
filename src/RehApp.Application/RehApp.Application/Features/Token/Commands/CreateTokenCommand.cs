using MediatR;
using Microsoft.AspNetCore.Identity;
using RehApp.Domain.RelationalDatabase.Entities;
using RehApp.Infrastructure.Common;
using RehApp.Infrastructure.Common.Exceptions;
using RehApp.Infrastructure.Common.Extensions;
using RehApp.Infrastructure.Common.Helpers;
using RehApp.Infrastructure.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Models = RehApp.Infrastructure.Common.Models;

namespace RehApp.Application.Features.Token.Commands;

public class CreateTokenRequest : IRequest<InternalResponse<Models.Token>>
{
    /// <summary>
    /// In this field, you can specify the username or mailbox of the user
    /// </summary>
    [Required]
    public string Login { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}

public class CreateTokenCommandHandler
    : IRequestHandler<CreateTokenRequest, InternalResponse<Models.Token>>
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly BearerSettings bearerSettings;

    public CreateTokenCommandHandler(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        BearerSettings bearerSettings)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.bearerSettings = bearerSettings;
    }

    public async Task<InternalResponse<Models.Token>> Handle(
        CreateTokenRequest request,
        CancellationToken cancellationToken)
    {
        var response = new InternalResponse<Models.Token>();

        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return response.Failure(validation);

        var user = await userManager.FindByEmailAsync(request.Login)
            ?? await userManager.FindByNameAsync(request.Login);

        if (user is null) return response.Failure(Exceptions.InvalidLogInData);

        if (!(await signInManager.CheckPasswordSignInAsync(user, request.Password, false)).Succeeded)
        {
            return response.Failure(Exceptions.InvalidLogInData);
        }

        var claims = new List<Claim>()
        {
            new Claim("Id", user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, $"{user.UserName}"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var now = DateTime.UtcNow;
        
        var accessToken = TokenHelper.GenerateAccessToken(
            key: bearerSettings.Key,
            issuer: bearerSettings.Issuer,
            audience: bearerSettings.Audience,
            expires: DateTime.UtcNow.AddSeconds(bearerSettings.AccessTokenLifetime),
            claims: claims);

        if (user.RefreshToken is null || user.RefreshTokenExpiryTime < now)
        {
            var refreshToken = TokenHelper.GenerateRefreshToken();
            var refreshTokenExpiryTime = now.AddSeconds(bearerSettings.RefreshTokenLifetime);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = refreshTokenExpiryTime;
            await userManager.UpdateAsync(user);
        }

        return response.Success(new Models.Token
        {
            AccessToken = accessToken,
            RefreshToken = user.RefreshToken
        });
    }
}