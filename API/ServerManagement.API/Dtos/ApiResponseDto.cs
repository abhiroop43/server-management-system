namespace ServerManagement.API.Dtos;

public record ApiResponseDto(int Status, string Title, dynamic? Detail);
