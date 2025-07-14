namespace Gym.Core.Dtos;

public record ExpiringSubDto(
    long SubscriptionId,
    long MemberId,
    string Name,
    DateOnly EndDate,
    int GraceDays,
    string Email);
