using RehApp.Infrastructure.Common.Interfaces;

namespace RehApp.Domain.RelationalDatabase.Entities;

public class Training : IIdentified
{
    public Guid Id { get; set; }

    public Guid DiaryId { get; set; }
    public Guid SpecialistId { get; set; }

    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
}
