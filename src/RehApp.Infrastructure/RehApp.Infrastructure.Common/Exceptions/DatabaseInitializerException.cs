namespace RehApp.Infrastructure.Common.Exceptions;

public class DatabaseInitializerException : Exception
{
    private const string MESSAGE = "error during database initialization execution";
    public DatabaseInitializerException(string message = MESSAGE) : base(message) { }
}
