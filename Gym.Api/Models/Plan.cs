
// =============================
// File: Models/Plan.cs
// =============================
namespace Gym.Api.Models;

public class Plan
{
    public int PlanId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int PriceCents { get; set; }
    public byte DurationMonths { get; set; }
}
