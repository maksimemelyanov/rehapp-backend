using RehApp.Infrastructure.Common.Interfaces;

namespace RehApp.Domain.RelationalDatabase.Entities;

public class ExerciseParamsDesc : IIdentified, IDescription
{
    public Guid Id { get; set; }

    public Guid ExerciseParamsId { get; set; }
    public virtual ExerciseParams ExerciseParams { get; set; } = null!;

    public Guid DescriptionTypeId { get; set; }
    public virtual DescriptionType DescriptionType { get; set; } = null!;

    public Guid DescriptionValueId { get; set; }
    public virtual DescriptionValue DescriptionValue { get; set; } = null!;
}
