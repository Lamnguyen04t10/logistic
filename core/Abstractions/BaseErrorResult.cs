namespace Core.Abstractions
{
    public class BaseErrorResult
    {
        public int StatusCode { get;set;}
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}
