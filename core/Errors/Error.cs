namespace Core.Errors
{
    public class Error
    {
        public string Code { get; }
        public string Message { get; }

        private Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public static Error Create(string code, string message) => new(code, message);

        public static Error Validation(string message) => new("VALIDATION_ERROR", message);

        public static Error NotFound(string message) => new("NOT_FOUND", message);

        public static Error Conflict(string message) => new("CONFLICT", message);

        public static Error Unauthorized(string message) => new("UNAUTHORIZED", message);

        public static Error Internal(string message) => new("INTERNAL_ERROR", message);
    }
}
