namespace BookManagement.Shared.Models;

public class Result<TError>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public TError? Error { get; }

    protected Result(bool isSuccess, TError? error = default)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result<TError> Success()
    {
        return new Result<TError>(true);
    }

    public static Result<TError> Failure(TError error)
    {
        return new Result<TError>(false, error);
    }

    public static implicit operator Result<TError>(TError error) => new(false, error);
}

public class Result<TValue, TError> : Result<TError>
{
    public TValue? Data { get; private set; }

    private Result(TValue data) : base(true)
    {
        Data = data;
    }

    private Result(TError error) : base(false, error)
    {
    }

    public static Result<TValue, TError> Success(TValue resultData)
    {
        return new Result<TValue, TError>(resultData);
    }

    public new static Result<TValue, TError> Failure(TError error)
    {
        return new Result<TValue, TError>(error);
    }

    public static implicit operator Result<TValue, TError>(TValue data) => new(data);
    public static implicit operator Result<TValue, TError>(TError error) => new(error);
}