using RehApp.Domain.RelationalDatabase.ValueObjects;
using RehApp.Infrastructure.Common.Interfaces;

namespace RehApp.Domain.RelationalDatabase.Entities;

public class DiaryEntry : IIdentified
{
    public Guid Id { get; set; }

    public Guid DiaryId { get; set; }
    public Guid? TrainingId { get; set; }

    public DateTime CreationDate { get; set; }
    public DateTime? Date { get; set; }

    public int Pulse { get; set; }
    public Pressure Pressure { get; set; } = null!;
    public string? Note { get; set; }
}
