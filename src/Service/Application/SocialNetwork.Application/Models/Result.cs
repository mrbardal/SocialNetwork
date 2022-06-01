namespace SocialNetwork.Application.Models;

public class Result
{
    public bool IsSuccess { get; private set; }
    public string Error { get; private set; }

    protected Result(bool isSuccess)
    {
        IsSuccess = isSuccess;
        Error = string.Empty;
    }
    protected Result(string error)
    {
        IsSuccess = false;
        Error = error;
    }

    public static Result Fail(string error) => new Result(error);

    public static Result<T> Success<T>(T value) => new Result<T>(value, true);
}

public class Result<T> : Result
{
    public T Value { get; set; }

    protected internal Result(T value, bool success) : base(success)
    {
        Value = value;
    }

    protected internal Result(T value, string error) : base(error)
    {
        Value = value;
    }
}
