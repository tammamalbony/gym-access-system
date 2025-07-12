namespace Gym.Client.Models;

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
