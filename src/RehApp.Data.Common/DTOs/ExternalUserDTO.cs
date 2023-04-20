using RehApp.Infrastructure.Common.Interfaces;

namespace RehApp.Data.Common.DTOs;

public class ExternalUserDTO : IDTO
{
    public string? FirstName { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public string Username { get; set; } = null!;
}
