
// =============================
// File: Dtos/PlanDto.cs
// =============================
namespace Gym.Api.Dtos;

public record PlanDto(
    int Id,
    string Name,
    string? Description,
    int PriceCents,
    byte DurationMonths,
    byte GraceDays,
    bool IsActive,
    DateTime CreatedAt,
    DateTime UpdatedAt);
