using Microsoft.AspNetCore.Identity;

namespace RehApp.Domain.RelationalDatabase.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    public virtual ICollection<ExtAuthInfo> ExtAuthInfo { get; set; } = null!;
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
}
