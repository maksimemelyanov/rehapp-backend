using RehApp.Domain.RelationalDatabase.Enums;

namespace RehApp.Domain.RelationalDatabase.Entities;

public class ExtAuthInfo
{
    public Guid Id { get; set; }
    public string ExternalId { get; set; } = null!;
    public AuthProvider Provider { get; set;}
}
