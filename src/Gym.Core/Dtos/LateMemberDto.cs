namespace Gym.Core.Dtos;

public record LateMemberDto(
    long SubscriptionId,
    long MemberId,
    string Name,
    int DueCents,
    int DaysLate);
