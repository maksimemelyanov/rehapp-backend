using RehApp.Domain.RelationalDatabase.Abstractions;
using RehApp.Infrastructure.Common.Interfaces;

namespace RehApp.Domain.RelationalDatabase.Entities;

public class ExerciseParamsHistory : IIdentified, IHistoryEntity
{
    public Guid Id { get; set; }

    public Guid ExerciseParamsId { get; set; }
    public string ActionDescription { get; set; } = null!;
}
