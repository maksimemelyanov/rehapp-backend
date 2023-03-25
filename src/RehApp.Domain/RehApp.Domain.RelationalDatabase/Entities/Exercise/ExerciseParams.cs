using RehApp.Infrastructure.Common.Interfaces;

namespace RehApp.Domain.RelationalDatabase.Entities;

public class ExerciseParams : IIdentified
{
    public Guid Id { get; set; }

    public Guid TrainingId { get; set; }
    public Guid ExerciseId { get; set; }
}
