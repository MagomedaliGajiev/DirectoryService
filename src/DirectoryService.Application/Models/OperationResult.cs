using Shared;

namespace DirectoryService.Application.Models;

public class OperationResult<T>
{
    private OperationResult(bool isSuccess, T? value, Error? error)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }

    public bool IsSuccess { get; private set; }

    public T? Value { get; private set; }

    public Error? Error { get; private set; }

    public static OperationResult<T> Success(T value) => new(true, value, null);

    public static OperationResult<T> Failure(Error error) => new(false, default, error);
}
