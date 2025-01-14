namespace SchoolManagementSystem.Shared.WebServices
{
    public class Result<T>
    {
        public bool IsSuccess { get; private init; }
        public bool IsFailure => !IsSuccess;
        public string? Message { get; private init; }
        public int StatusCode { get; private init; }
        public T? Data { get; private init; }

        private Result(bool isSuccess, string? message, T? data, int statusCode)
        {
            IsSuccess = isSuccess;
            Message = message;
            StatusCode = statusCode;
            Data = data;
        }

        public static Result<T> Success(T data, int statusCode)
        {
            return new Result<T>(true, null, data, statusCode);
        }

        public static Result<T> Failure(string message, int statusCode)
        {
            return new Result<T>(false, message, default, statusCode);
        }
    }

    public class Result
    {
        public bool IsSuccess { get; private init; }
        public bool IsFailure => !IsSuccess;
        public string? Message { get; private init; }
        public int StatusCode { get; private init; }
        private Result(bool isSuccess, string? message, int statusCode)
        {
            IsSuccess = isSuccess;
            Message = message;
            StatusCode = statusCode;
        }
        public static Result Success(int statusCode)
        {
            return new Result(true, null, statusCode);
        }
        public static Result Failure(string message, int statusCode)
        {
            return new Result(false, message, statusCode);
        }
    }
}
