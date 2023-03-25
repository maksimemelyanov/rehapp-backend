using RehApp.Infrastructure.Common.Interfaces;

namespace RehApp.Domain.RelationalDatabase.Entities;

public class Invitation : IIdentified
{
    public Guid Id { get; set; }

    public Guid SenderId { get; set; }
    public virtual ApplicationUser Sender { get; set; } = null!;

    public Guid RecipientId { get; set; }
    public virtual ApplicationUser Recipient { get; set; } = null!;
}
