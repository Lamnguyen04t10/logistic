using Core.Errors;

namespace Core.Result
{
    public interface IResult
    {
        bool IsSuccess { get; }
        Error Error { get; }
    }
}
