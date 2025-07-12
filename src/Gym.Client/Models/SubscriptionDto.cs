namespace Gym.Client.Models;

public record SubscriptionDto(
    long Id,
    long MemberId,
    int PlanId,
    DateOnly StartDate,
    DateOnly EndDate,
    string Status,
    DateTime CreatedAt,
    DateTime UpdatedAt);
