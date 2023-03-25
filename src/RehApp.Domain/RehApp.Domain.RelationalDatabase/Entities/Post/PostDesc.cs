using RehApp.Infrastructure.Common.Interfaces;

namespace RehApp.Domain.RelationalDatabase.Entities;

public class PostDesc : IIdentified, IDescription
{
    public Guid Id { get; set; }

    public Guid PostId { get; set; }
    public virtual Post Post { get; set; } = null!;

    public Guid DescriptionTypeId { get; set; }
    public virtual DescriptionType DescriptionType { get; set; } = null!;

    public Guid DescriptionValueId { get; set; }
    public virtual DescriptionValue DescriptionValue { get; set; } = null!;
}
