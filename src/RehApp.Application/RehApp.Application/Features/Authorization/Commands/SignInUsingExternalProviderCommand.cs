using MediatR;
using Microsoft.AspNetCore.Identity;
using RehApp.Application.Extensions;
using RehApp.Domain.RelationalDatabase.Entities;
using RehApp.Domain.RelationalDatabase.Enums;
using RehApp.Infrastructure.Common;
using RehApp.Infrastructure.Common.Enums;
using RehApp.Infrastructure.Common.Exceptions;
using RehApp.Infrastructure.Common.Extensions;
using RehApp.Infrastructure.Common.Helpers;
using RehApp.Infrastructure.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace RehApp.Application.Features.Authorization.Commands;

public class SignInUsingExternalProviderRequest : IRequest<InternalResponse<string>> 
{
    [Required]
    public string Callback { get; set; } = null!;

    [Required]
    public AuthMethod AuthMethod { get; set; }
}

public class SignInUsingExternalProviderCommandHandler
    : IRequestHandler<SignInUsingExternalProviderRequest, InternalResponse<string>>
{
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly BearerSettings bearerSettings;

    public SignInUsingExternalProviderCommandHandler(
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        BearerSettings bearerSettings)
    {
        this.signInManager = signInManager;
        this.userManager = userManager;
        this.bearerSettings = bearerSettings;
    }

    public async Task<InternalResponse<string>> Handle(
        SignInUsingExternalProviderRequest request,
        CancellationToken cancellationToken)
    {
        var response = new InternalResponse<string>();

        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return response.Failure(validation);

        var info = await signInManager.GetExternalLoginInfoAsync();
        if (info is null) return response.Failure(Exceptions.FailedToGetUserData);

        var user = await GetUserByExternalLoginInfoAsync(info);
        if (user is null) return response.Failure(Exceptions.FailedToIdentifyUser);

        if (await userManager.FindUserLoginInfoAsync(user, info.LoginProvider, info.ProviderKey) is null)
        {
            var identityResult = await userManager.AddLoginAsync(user, info);
            if (!identityResult.Succeeded)
            {
                var message = string.Join("; ", identityResult.Errors);
                return response.Failure(new InvalidOperationException(message));
            }
        }

        return request.AuthMethod == AuthMethod.Cookie
            ? await CookieAuthAsync(response, info, user, request.Callback)
            : await TokenAuthAsync(response, user, request.Callback);
    }

    private async Task<ApplicationUser?> GetUserByExternalLoginInfoAsync(ExternalLoginInfo info)
    {
        var provider = Enum.Parse<AuthProvider>(info.LoginProvider);
        var externalId = info.Principal.Claims.GetByClaimType(ClaimTypes.NameIdentifier);

        return await userManager.FindByExternalLoginInfoAsync(provider, externalId!);
    }

    private async Task<InternalResponse<string>> CookieAuthAsync(
        InternalResponse<string> response,
        ExternalLoginInfo info,
        ApplicationUser user,
        string callback)
    {
        var signInResult = await signInManager.ExternalLoginSignInAsync(
            info.LoginProvider,
            info.ProviderKey,
            true);

        if (!signInResult.Succeeded) return response.Failure(Exceptions.FailedToSignIn);

        await signInManager.SignInAsync(user, true);

        return response.Success(callback);
    }

    private async Task<InternalResponse<string>> TokenAuthAsync(
        InternalResponse<string> operation,
        ApplicationUser user,
        string callback)
    {
        if (user.RefreshToken is null) await AssignToUserRefreshTokenAsync(user);

        var accessTokenExpiryTime = DateTime.UtcNow.AddSeconds(bearerSettings.AccessTokenLifetime);
        var accessToken = GenerateAccessToken(user);

        var url = BuildCallback(
            $"{callback}",
            accessToken,
            accessTokenExpiryTime,
            user.RefreshToken!,
            user.RefreshTokenExpiryTime!.Value);

        return operation.Success(url);
    }

    private async Task AssignToUserRefreshTokenAsync(ApplicationUser user)
    {
        var expiryTime = DateTime.UtcNow.AddSeconds(bearerSettings.RefreshTokenLifetime);

        user.RefreshToken = TokenHelper.GenerateRefreshToken();
        user.RefreshTokenExpiryTime = expiryTime;

        await userManager.UpdateAsync(user);
    }

    private string GenerateAccessToken(ApplicationUser user)
    {
        var claims = new List<Claim>()
        {
            new Claim("Id", user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var accessToken = TokenHelper.GenerateAccessToken(
            key: bearerSettings.Key,
            issuer: bearerSettings.Issuer,
            audience: bearerSettings.Audience,
            expires: DateTime.UtcNow.AddSeconds(bearerSettings.AccessTokenLifetime),
            claims: claims);

        return accessToken;
    }

    private static string BuildCallback(
        string callback,
        string accessToken,
        DateTime accessTokenExpiryTime,
        string refreshToken,
        DateTime refreshTokenExpiryTime)
    {
        if (string.IsNullOrEmpty(callback)) throw new ArgumentException("Callback was null");

        callback = callback.Split("?")[0];

        var parameters = new Dictionary<string, string>
        {
            { "accessToken", accessToken },
            { "refreshToken", refreshToken }
        };

        var urlParameters = string.Join("&", parameters
            .Where(kvp => !string.IsNullOrEmpty(kvp.Value))
            .Select(kvp => $"{WebUtility.UrlEncode(kvp.Key)}={WebUtility.UrlEncode(kvp.Value)}"));

        return string.Join("?", callback, urlParameters);
    }
}