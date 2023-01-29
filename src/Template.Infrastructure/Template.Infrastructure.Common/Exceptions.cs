using Template.Infrastructure.Common.Models;

namespace Template.Infrastructure.Common.Exceptions;

public static class Exceptions
{
    public static ArgumentException InvalidLogInData => new("Invalid login or password.");

    public static ArgumentException InvalidRefreshTokenData => new("Invalid access token or refresh token.");

    public static InvalidOperationException FailedToIdentifyUser => new("Failed to identify the user.");

    public static ArgumentException RefreshTokenExpired => new("Refresh token expired.");
}
