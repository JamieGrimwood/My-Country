namespace JupiterBankApi.Models;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public ApiError? Error { get; set; }

    // Static methods for easy creation
    public static ApiResponse<T> Ok(T data)
    {
        return new ApiResponse<T>
        {
            Success = true,
            Data = data,
            Error = null
        };
    }

    public static ApiResponse<T> Fail(string message)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Data = default,
            Error = new ApiError { Message = message }
        };
    }
}

// For responses without data
public class ApiResponse
{
    public bool Success { get; set; }
    public ApiError? Error { get; set; }

    public static ApiResponse Ok()
    {
        return new ApiResponse
        {
            Success = true,
            Error = null
        };
    }

    public static ApiResponse Fail(string message)
    {
        return new ApiResponse
        {
            Success = false,
            Error = new ApiError { Message = message }
        };
    }
}

public class ApiError
{
    public string? Message { get; set; }
}