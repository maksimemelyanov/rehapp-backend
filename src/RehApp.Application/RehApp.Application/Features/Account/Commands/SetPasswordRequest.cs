using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RehApp.Domain.RelationalDatabase.Entities;
using RehApp.Infrastructure.Common;
using RehApp.Infrastructure.Common.Exceptions;
using RehApp.Infrastructure.Common.Extensions;
using RehApp.Infrastructure.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace RehApp.Application.Features.Account.Commands;

public class SetPasswordRequest : IRequest<InternalResponse>
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public string Token { get; set; } = null!;

    [Required, MinLength(8)]
    public string Password { get; set; } = null!;

    [Required, Compare(nameof(Password))]
    public string PasswordConfirmation { get; set; } = null!;
}

public class ResetPasswordCommandHandler : IRequestHandler<SetPasswordRequest, InternalResponse>
{
    private readonly UserManager<ApplicationUser> userManager;

    public ResetPasswordCommandHandler(UserManager<ApplicationUser> userManager)
    {
        this.userManager = userManager;
    }

    public async Task<InternalResponse> Handle(
        SetPasswordRequest request,
        CancellationToken cancellationToken)
    {
        var operation = new InternalResponse();

        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return operation.Failure(validation);

        var user = await userManager.Users.SingleOrDefaultAsync(x => x.Id == request.UserId);
        if (user is null) return operation.Failure(Exceptions.FailedToIdentifyUser);

        var result = await userManager.ResetPasswordAsync(user, request.Token, request.Password);

        if (!result.Succeeded)
        {
            var message = string.Join("; ", result.Errors.Select(x => x.Description));
            return operation.Failure(new InvalidOperationException(message));
        }

        return operation.Success();
    }
}
