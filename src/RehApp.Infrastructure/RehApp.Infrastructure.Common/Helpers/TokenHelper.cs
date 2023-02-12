using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RehApp.Infrastructure.Common.Helpers;

public class TokenHelper
{
    public static string GenerateAccessToken(string key, string issuer, string audience, List<Claim> claims, DateTime expires)
    {
        var ssKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(ssKey, SecurityAlgorithms.HmacSha256);
        var jwtToken = new JwtSecurityToken(issuer, audience, claims, DateTime.UtcNow, expires, credentials);
        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }

    public static string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public static ClaimsPrincipal? GetPrincipalFromExpiredToken(string accessToken, TokenValidationParameters validationParameters)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(accessToken, validationParameters, out SecurityToken securityToken);
            var strComparasion = StringComparison.InvariantCultureIgnoreCase;

            return securityToken is not JwtSecurityToken jwtSecurityToken 
                || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, strComparasion)
                    ? null : principal;
        }
        catch { return null; }
    }
}
