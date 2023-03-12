namespace RehApp.Infrastructure.Common.Exceptions;

public static class Exceptions
{
    public static ArgumentException InvalidLogInData => new("Invalid login or password.");
    public static ArgumentException InvalidRefreshTokenData => new("Invalid access token or refresh token.");
    public static InvalidOperationException FailedToIdentifyUser => new("Failed to identify the user.");
    public static ArgumentException RefreshTokenExpired => new("Refresh token expired.");
    public static InvalidDataException FailedToGetUserData => new("User data could not be retrieved.");
    public static InvalidOperationException FailedToSignIn => new("Error when trying to sign in");
}
