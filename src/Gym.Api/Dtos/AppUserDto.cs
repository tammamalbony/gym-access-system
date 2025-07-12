namespace Gym.Api.Dtos;

public record AppUserDto(
    int Id,
    string Username,
    string Role,
    string Email,
    bool IsEnabled,
    DateTime CreatedAt);
