using RehApp.Infrastructure.Common.Interfaces;

namespace RehApp.Data.Common.DTOs;

public class AppealStatusDTO : IDTO
{
    public int? Value { get; set; }
    public string? Description { get; set; }
}
