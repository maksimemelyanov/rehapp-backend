using RehApp.Infrastructure.Common.Interfaces;

namespace RehApp.Domain.RelationalDatabase.Entities;

public class Exercise : IIdentified
{
    public Guid Id { get; set; }

    public Guid AuthorId { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
}
