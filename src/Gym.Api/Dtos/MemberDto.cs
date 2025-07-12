
// =============================
// File: Dtos/MemberDto.cs
// =============================
namespace Gym.Api.Dtos;

public record MemberDto(
    long Id,
    string FirstName,
    string LastName,
    string Email,
    string? Phone,
    DateOnly? DateOfBirth,
    string? IdScanPath,
    bool KycComplete,
    DateTime CreatedAt,
    DateTime UpdatedAt);
