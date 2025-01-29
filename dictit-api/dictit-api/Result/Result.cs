using System.Net;

namespace DictItApi.Result;

public class Result<T>
{
    public bool IsSuccess { get; }

    public T? Value { get; }

    public string? Error { get; }

    private Result(bool isSuccess, T? value, string? error)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }
    
    public static Result<T> Success(T value) => new(true, value, null);
    public static Result<T> Failure(HttpStatusCode statusCode, string errorMessage) => new(false, default, $"Error: Status: {statusCode}, Reason: {errorMessage}");
}
