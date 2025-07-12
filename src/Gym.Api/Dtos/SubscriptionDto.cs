namespace Gym.Api.Dtos;

using Gym.Api.Models;

public record SubscriptionDto(
    long Id,
    long MemberId,
    int PlanId,
    DateOnly StartDate,
    DateOnly EndDate,
    SubscriptionStatus Status,
    DateTime CreatedAt,
    DateTime UpdatedAt);
