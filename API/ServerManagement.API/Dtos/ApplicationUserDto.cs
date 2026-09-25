namespace ServerManagement.API.Dtos;

public record ApplicationUserDto(
    string Email,
    string FirstName,
    string LastName,
    DateTime? DateOfBirth
);
