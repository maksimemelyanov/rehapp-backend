using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using RehApp.Domain.RelationalDatabase.Entities;
using RehApp.Domain.RelationalDatabase.Enums;
using RehApp.Infrastructure.Common;
using RehApp.Infrastructure.Common.Attributes;
using RehApp.Infrastructure.Common.Enums;
using RehApp.Infrastructure.Common.Extensions;
using RehApp.Infrastructure.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace RehApp.Application.Features.Authorization.Commands;

public class ConfigureAuthPropertiesRequest
: IRequest<InternalResponse<AuthenticationProperties>>
{
    [Required, IsEnumName(typeof(AuthProvider))]

    public string Provider { get; set; } = null!;

    [Required]
    public string Callback { get; set; } = null!;

    [Required]
    public AuthMethod AuthMethod { get; set; }

    public ConfigureAuthPropertiesRequest(
        string provider,
        AuthMethod authMethod,
        string callback)
    {
        Provider = provider;
        Callback = callback;
        AuthMethod = authMethod;
    }
}

public class ConfigureAuthPropertiesCommandHandler
    : IRequestHandler<ConfigureAuthPropertiesRequest,
        InternalResponse<AuthenticationProperties>>
{
    private readonly SignInManager<ApplicationUser> signInManager;

    public ConfigureAuthPropertiesCommandHandler(
        SignInManager<ApplicationUser> signInManager)
    {
        this.signInManager = signInManager;
    }

    public Task<InternalResponse<AuthenticationProperties>> Handle(
        ConfigureAuthPropertiesRequest request,
        CancellationToken cancellationToken)
    {
        var response = new InternalResponse<AuthenticationProperties>();

        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return Task.FromResult(response.Failure(validation));

        var callbackUrl = $"{ExternalSignInHandlerRelativePath}" +
            $"?authMethod={(int)request.AuthMethod}" +
            $"&callback={WebUtility.UrlEncode(request.Callback)}";

        var properties = signInManager.ConfigureExternalAuthenticationProperties(request.Provider, callbackUrl);

        return Task.FromResult(response.Success(properties));
    }
}