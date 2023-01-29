using Microsoft.AspNetCore.Identity;

namespace Template.Domain.RelationalDatabase.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    public string? RefreshToken { get; set; }

    public DateTime? RefreshTokenExpiryTime { get; set; }
}
