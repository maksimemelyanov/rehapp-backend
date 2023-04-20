using RehApp.Infrastructure.Common.Interfaces;

namespace RehApp.Data.Common.DTOs;

public class PostDTO : IDTO
{
    public DateTime Date { get; set; }
    public string? Text { get; set; }
    public List<string>? VideoUrls { get; set; }
    public List<string>? ImageUrls { get; set; }
    public List<string>? AudioUrls { get; set; }
}
