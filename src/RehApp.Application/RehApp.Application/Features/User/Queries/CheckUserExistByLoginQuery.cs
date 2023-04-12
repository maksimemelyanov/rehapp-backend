using MediatR;
using Microsoft.AspNetCore.Identity;
using RehApp.Domain.RelationalDatabase.Entities;
using RehApp.Infrastructure.Common;
using RehApp.Infrastructure.Common.Extensions;
using RehApp.Infrastructure.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace RehApp.Application.Features.User.Queries;

public class CheckUserExistByLoginRequest : IRequest<InternalResponse<bool>> 
{
    /// <summary>
    /// In this field, you can specify the username or mailbox of the user
    /// </summary>
    [Required]
    public string Login { get; set; } = null!;
}

public class CheckUserExistByLoginQueryHandler
    : IRequestHandler<CheckUserExistByLoginRequest, InternalResponse<bool>>
{
    private readonly UserManager<ApplicationUser> userManager;

    public CheckUserExistByLoginQueryHandler(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        this.userManager = userManager;
    }

    public async Task<InternalResponse<bool>> Handle(
        CheckUserExistByLoginRequest request,
        CancellationToken cancellationToken)
    {
        var response = new InternalResponse<bool>();

        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return response.Failure(validation);

        var user = await userManager.FindByEmailAsync(request.Login)
            ?? await userManager.FindByNameAsync(request.Login);

        return response.Success(user is not null);
    }
}
