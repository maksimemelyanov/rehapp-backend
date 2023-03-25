using RehApp.Infrastructure.Common.Interfaces;

namespace RehApp.Domain.RelationalDatabase.Entities;

public class Notification : IIdentified
{
    public Guid Id { get; set; }

    public DateTime Date { get; set; }
    public string Text { get; set; } = null!;
    public bool Viewed { get; set; }
    public Guid RecipientId { get; set; }
}
