namespace RehApp.Infrastructure.Common.Models;

public class BearerSettings
{
    public string Key { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public long AccessTokenLifetime { get; set; }
    public long RefreshTokenLifetime { get; set; }
}