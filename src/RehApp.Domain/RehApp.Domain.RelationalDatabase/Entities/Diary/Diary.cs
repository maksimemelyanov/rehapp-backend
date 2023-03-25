using RehApp.Infrastructure.Common.Interfaces;

namespace RehApp.Domain.RelationalDatabase.Entities;

public class Diary : IIdentified
{
    public Guid Id { get; set; }
    
    public Guid PatientId { get; set; }
    public virtual ApplicationUser Patient { get; set; } = null!;
}
