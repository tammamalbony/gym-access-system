namespace Gym.Client.Models;

public record ExpiringSubDto(
    long SubscriptionId,
    long MemberId,
    string Name,
    DateOnly EndDate,
    int GraceDays,
    string Email);
