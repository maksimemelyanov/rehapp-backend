using RehApp.Infrastructure.Common.Interfaces;

namespace RehApp.Domain.RelationalDatabase.Entities;

public class Review : IIdentified
{
    public Guid Id { get; set; }

    public DateTime Date { get; set; }

    public Guid AuthorId { get; set; }
    public ApplicationUser Author { get; set; } = null!;

    public Guid SpecialistId { get; set; }
    public ApplicationUser Specialist { get; set; } = null!;

    public double Evaluation { get; set; }
    public string? Text { get; set; }
}
