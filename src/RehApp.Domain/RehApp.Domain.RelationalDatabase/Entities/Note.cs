using RehApp.Infrastructure.Common.Interfaces;

namespace RehApp.Domain.RelationalDatabase.Entities;

public class Note : IIdentified
{
    public Guid Id { get; set; }

    public Guid SpecialistId { get; set; }
    public virtual ApplicationUser Specialist { get; set; } = null!;

    public Guid PatientId { get; set; }
    public virtual ApplicationUser Patient { get; set; } = null!;

    public string Text { get; set; } = null!;
    public bool TransferAllowed { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? Date { get; set; }
}
