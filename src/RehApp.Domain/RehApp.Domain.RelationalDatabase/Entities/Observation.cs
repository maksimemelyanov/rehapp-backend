using RehApp.Infrastructure.Common.Interfaces;

namespace RehApp.Domain.RelationalDatabase.Entities;

public class Observation : IIdentified
{
    public Guid Id { get; set; }

    public Guid PatientId { get; set; }
    public Guid SpecialistId { get; set; }

    public DateTime BeginDate { get; set; }
    public DateTime EndDate { get; set; }
}
