using RehApp.Domain.RelationalDatabase.Entities;

namespace RehApp.Domain.RelationalDatabase.Abstractions;

public interface IDescriptionEntity
{
    public Guid ParentId { get; set; }

    public DateTime CreationDate { get; set; }

    public Guid DescriptionTypeId { get; set; }
    public DescriptionType DescriptionType { get; set; }

    public Guid DescriptionValueId { get; set; }
    public DescriptionValue DescriptionValue { get; set; }
}
