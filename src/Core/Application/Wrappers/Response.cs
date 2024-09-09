using Application.Constant;

namespace Application.Wrappers
{
    public class Response<T> where T : class
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; } = [];
        public T Data { get; set; }
        public Response() { }
        public Response(bool succeeded, string message, List<string> errors, T data)
        {
            IsSuccess = succeeded;
            Message = message;
            Errors = errors;
            Data = data;
        }
        public Response(bool succeeded, string message, List<string> errors)
        {
            IsSuccess = succeeded;
            Message = message;
            Errors = errors;
        }

        public static Response<T> NotSuccess(string message)
        {
            return new Response<T>(false, message, Enumerable.Empty<string>().ToList());
        }
        public static Response<T> NotSuccess(string message, string[] errors)
        {
            return new Response<T>(false, message, [.. errors]);
        }
        public static Response<T> Success(T data, string message)
        {
            return new Response<T>(true, message, Enumerable.Empty<string>().ToList(), data);
        }
        public static Response<T> Success(T data)
        {
            return new Response<T>(true, ResponseMessages.SuccessMessage, Enumerable.Empty<string>().ToList(), data);
        }
    }
}
