using MediatR;
using Microsoft.AspNetCore.Identity;
using RehApp.Domain.RelationalDatabase.Entities;
using RehApp.Infrastructure.Common;
using RehApp.Infrastructure.Common.Exceptions;
using RehApp.Infrastructure.Common.Extensions;
using RehApp.Infrastructure.Common.Models;
using RehApp.Infrastructure.MailService;
using RehApp.Infrastructure.MailService.Models;
using System.ComponentModel.DataAnnotations;

namespace RehApp.Application.Features.Account.Commands;

public class SendPasswordResetMailRequest : IRequest<InternalResponse>
{
    [Required, EmailAddress]

    public string Email { get; set; } = null!;

    [Required]
    public string Callback { get; set; } = null!;
}

public class SendPasswordResetMailCommandHandler
    : IRequestHandler<SendPasswordResetMailRequest, InternalResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMailService _mailService;

    public SendPasswordResetMailCommandHandler(
        UserManager<ApplicationUser> userManager,
        IMailService mailService)
    {
        _userManager = userManager;
        _mailService = mailService;
    }

    public async Task<InternalResponse> Handle(
        SendPasswordResetMailRequest request,
        CancellationToken cancellationToken)
    {
        var operation = new InternalResponse();

        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return operation.Failure(validation);

        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null) return operation.Failure(Exceptions.FailedToIdentifyUser);

        if (!await _userManager.IsEmailConfirmedAsync(user))
        {
            return operation.Failure(Exceptions.UnconfirmedMailbox);
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        var url = $"{request.Callback}?userId={user.Id}&token={token}";

        await _mailService.SendAsync(
            MailMessage.ResetPassword(request.Email, url!),
            cancellationToken);

        return operation.Success();
    }
}
