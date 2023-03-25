using RehApp.Infrastructure.Common.Interfaces;

namespace RehApp.Domain.RelationalDatabase.Entities;

public class TrainingHistory : IIdentified, IHistory
{
    public Guid Id { get; set; }

    public Guid TrainingId { get; set; }
    public string ActionDescription { get; set; } = null!;
}
