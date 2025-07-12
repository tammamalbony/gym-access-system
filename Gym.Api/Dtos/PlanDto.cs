
// =============================
// File: Dtos/PlanDto.cs
// =============================
namespace Gym.Api.Dtos;

public record PlanDto(int Id, string Name, int PriceCents, byte DurationMonths);
