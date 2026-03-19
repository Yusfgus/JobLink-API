namespace JobLink.Domain.Common.Results;

public class Result
{
    public List<Error> Errors { get; private set; } = default!;
    public bool IsSuccess { get; private set; }
    public bool IsFailure => !IsSuccess;

    protected Result()
    {
        Errors = [];
        IsSuccess = true;
    }

    protected Result(List<Error> errors)
    {
        Errors = errors ?? throw new ArgumentNullException(nameof(errors));
        IsSuccess = false;
    }

    public static Result Success() => new();

    public static Result Combine(params Result[] results)
    {
        List<Error> errors = results
            .Where(r => r.IsFailure)
            .SelectMany(r => r.Errors)
            .ToList();

        return errors.Count > 0 ? errors : Success();
    }

    public static implicit operator Result(Error error) => new([error]);
    public static implicit operator Result(List<Error> errors) => new(errors);

    public TResult Match<TResult>(Func<TResult> onSuccess, Func<List<Error>, TResult> onFailure)
        => IsSuccess ? onSuccess() : onFailure(Errors);
}

public class Result<T> : Result
{
    public T? Value { get; private set; }

    private Result(List<Error> errors) : base(errors)
    {
        Value = default;
    }

    private Result(T value)
    {
        Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    public static Result<T> Success(T value) => new(value);

    public static implicit operator Result<T>(T value) => Success(value);
    public static implicit operator Result<T>(Error error) => new([error]);
    public static implicit operator Result<T>(List<Error> errors) => new(errors);

    public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<List<Error>, TResult> onFailure)
        => IsSuccess ? onSuccess(Value!) : onFailure(Errors);
}