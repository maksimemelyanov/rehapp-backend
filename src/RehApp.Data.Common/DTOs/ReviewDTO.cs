using RehApp.Infrastructure.Common.Interfaces;

namespace RehApp.Data.Common.DTOs;

public class ReviewDTO : IDTO
{
    public ApplicationUserMinDTO Author { get; set; } = null!;

    public DateTime Date { get; set; }
    public string? Text { get; set; }
    public double Evaluation { get; set; }

    public List<string>? VideoUrls { get; set; }
    public List<string>? ImageUrls { get; set; }
}
