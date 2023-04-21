using RehApp.Domain.RelationalDatabase.Enums;
using RehApp.Infrastructure.Common.Interfaces;

namespace RehApp.Domain.RelationalDatabase.Entities;

public class Appeal : IIdentified
{
    public Guid Id { get; set; }

    public AppealType Type { get; set; }
    public AppealStatus Status { get; set; }

    public Guid AuthorId { get; set; }
    public virtual ApplicationUser Author { get; set; } = null!;

    public DateTime Date { get; set; }
    public string? Text { get; set; }
}
