namespace Task_EMCO.Server.Common;

public class Result<T>
{
    public bool Success { get; set; }
    public T Data { get; set; }
    public string Message { get; set; }

    public Result(T data, bool success = true, string message = null)
    {
        Success = success;
        Data = data;
        Message = message;
    }

    public static Result<T> SuccessResult(T data)
    {
        return new Result<T>(data, true);
    }

    public static Result<T> ErrorResult(string message)
    {
        return new Result<T>(default, false, message);
    }
}