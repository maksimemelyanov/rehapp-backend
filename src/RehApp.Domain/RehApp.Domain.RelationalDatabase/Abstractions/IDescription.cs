using RehApp.Domain.RelationalDatabase.Entities;

namespace RehApp.Infrastructure.Common.Interfaces;

public interface IDescription
{
    public Guid DescriptionTypeId { get; set; }
    public DescriptionType DescriptionType { get; set; }

    public Guid DescriptionValueId { get; set; }
    public DescriptionValue DescriptionValue { get; set; } 
}
