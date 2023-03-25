using RehApp.Infrastructure.Common.Interfaces;

namespace RehApp.Domain.RelationalDatabase.Entities;

public class ExerciseHistory : IIdentified, IHistory
{
    public Guid Id { get; set; }
    public Guid ExerciseId { get; set; }
    public string ActionDescription { get; set; } = null!;
}
