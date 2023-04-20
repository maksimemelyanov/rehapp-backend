using Microsoft.AspNetCore.Identity;
using RehApp.Infrastructure.Common.Interfaces;

namespace RehApp.Domain.RelationalDatabase.Entities;

public class ApplicationUser : IdentityUser<Guid>, IIdentified
{
    public string AvatarUrl { get; set; } = null!;

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    
    public bool Verified { get; set; }
    public bool Blocked { get; set; }
    public bool Deleted { get; set; }

    public Guid CityId { get; set; }
    public virtual City City { get; set; } = null!;

    public virtual ICollection<ExtAuthInfo> ExtAuthInfo { get; set; } = null!;
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
}
