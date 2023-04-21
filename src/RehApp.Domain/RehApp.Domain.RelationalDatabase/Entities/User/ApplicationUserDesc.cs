using RehApp.Domain.RelationalDatabase.Abstractions;
using RehApp.Infrastructure.Common.Interfaces;

namespace RehApp.Domain.RelationalDatabase.Entities;

public class ApplicationUserDesc : IIdentified, IDescriptionEntity
{
    public Guid Id { get; set; }

    public DateTime CreationDate { get; set; }

    public Guid ParentId { get; set; }
    public virtual ApplicationUser Parent { get; set; } = null!;

    public Guid DescriptionTypeId { get; set; }
    public virtual DescriptionType DescriptionType { get; set; } = null!;

    public Guid DescriptionValueId { get; set; }
    public virtual DescriptionValue DescriptionValue { get; set; } = null!;
}
