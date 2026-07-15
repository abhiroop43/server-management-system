namespace ServerManagement.API.Dtos;

public record ApiResponseDto(int StatusCode, string Message, dynamic? Data);
