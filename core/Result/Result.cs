using Core.Errors;

namespace Core.Result
{
    public class Result<T> : IResult
    {
        public bool IsSuccess { get; }
        public Error Error { get; }
        public List<string> ValidationErrors { get; } = new();
        public T Value { get; }

        protected Result(bool isSuccess, Error error, List<string> validationErrors, T value)
        {
            IsSuccess = isSuccess;
            Error = error;
            ValidationErrors = validationErrors ?? new();
            Value = value;
        }

        public static Result<T> Success(T value) => new(true, null, null, value);

        public static Result<T> Failure(Error error) => new(false, error, null, default);

        public static Result<T> ValidationFailure(List<string> validationErrors) =>
            new(false, Error.Validation("Validation failed"), validationErrors, default);

        public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<Error, TResult> onFailure) =>
            IsSuccess ? onSuccess(Value) : onFailure(Error);

        public Result<TResult> Map<TResult>(Func<T, TResult> map) =>
            IsSuccess ? Result<TResult>.Success(map(Value)) : Result<TResult>.Failure(Error);
    }

    public class Result : IResult
    {
        public bool IsSuccess { get; }
        public Error Error { get; }
        public List<string> ValidationErrors { get; } = new();

        protected Result(bool isSuccess, Error error, List<string> validationErrors)
        {
            IsSuccess = isSuccess;
            Error = error;
            ValidationErrors = validationErrors ?? new();
        }

        public static Result Success() => new(true, null, null);

        public static Result Failure(Error error) => new(false, error, null);

        public static Result ValidationFailure(List<string> validationErrors) =>
            new(false, Error.Validation("Validation failed"), validationErrors);

        public TResult Match<TResult>(Func<TResult> onSuccess, Func<Error, TResult> onFailure) =>
            IsSuccess ? onSuccess() : onFailure(Error);
    }
}
