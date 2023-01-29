using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Template.Domain.RelationalDatabase.Entities;
using Template.Infrastructure.Common;
using Template.Infrastructure.Common.Exceptions;
using Template.Infrastructure.Common.Extensions;
using Template.Infrastructure.Common.Models;

namespace Template.Application.Features.Authorization.Commands;

public class SignInRequest : IRequest<InternalResponse>
{
    [Required]
    public string Login { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}

public class SignInCommandHandler : IRequestHandler<SignInRequest, InternalResponse>
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly SignInManager<ApplicationUser> signInManager;

    public SignInCommandHandler(
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager)
    {
        this.signInManager = signInManager;
        this.userManager = userManager;
    }

    public async Task<InternalResponse> Handle(SignInRequest request, CancellationToken cancellationToken)
    {
        var response = new InternalResponse();

        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return response.Failure(validation);

        var user = await userManager.FindByEmailAsync(request.Login)
            ?? await userManager.FindByNameAsync(request.Login);

        if (user is null) return response.Failure(Exceptions.InvalidLogInData);

        var signInResult = await signInManager.PasswordSignInAsync(user, request.Password, true, false);

        if (!signInResult.Succeeded) return response.Failure(Exceptions.InvalidLogInData);

        return response.Success();
    }
}