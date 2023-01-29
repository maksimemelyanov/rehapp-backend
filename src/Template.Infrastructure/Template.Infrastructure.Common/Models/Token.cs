using System.Text.Json.Serialization;

namespace Template.Infrastructure.Common.Models;

public class Token
{
    [JsonPropertyName("accessToken")]
    public string AccessToken { get; set; } = null!;

    [JsonPropertyName("refreshToken")]
    public string RefreshToken { get; set; } = null!;
}