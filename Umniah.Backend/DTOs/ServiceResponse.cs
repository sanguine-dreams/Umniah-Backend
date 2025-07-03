namespace Umniah.Backend.DTOs;

public class ServiceResponse<T>(bool isSuccess, string message, T? data = default)
{
    public bool IsSuccess { get; set; } = isSuccess;
    public string Message { get; set; } = message;
    public T? Data { get; set; } = data;
}