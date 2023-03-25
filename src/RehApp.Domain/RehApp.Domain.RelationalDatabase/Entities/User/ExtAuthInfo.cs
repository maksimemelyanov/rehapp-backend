using RehApp.Domain.RelationalDatabase.Enums;
using RehApp.Infrastructure.Common.Interfaces;

namespace RehApp.Domain.RelationalDatabase.Entities;

public class ExtAuthInfo : IIdentified
{
    public Guid Id { get; set; }
    public string ExternalId { get; set; } = null!;
    public AuthProvider Provider { get; set;}
}
