namespace RehApp.Infrastructure.Common.Models;

public class FailureResponse
{
    public string Message { get; } = null!;
    public object? Data { get; }

    public FailureResponse(string message, object? data = null)
    {
        Message = message;
        Data = data;
    }

    public FailureResponse(InternalResponse internalResponse, object? data = null)
    {
        if (internalResponse is null)
        {
            throw new ArgumentException("Internal response was null.");
        }

        if (string.IsNullOrEmpty(internalResponse.Exception?.Message))
        {
            throw new ArgumentException("Error description was not specified.");
        }

        Message = internalResponse!.Exception!.Message;
        Data = data;
    }
}
