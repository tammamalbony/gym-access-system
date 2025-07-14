namespace Gym.Core.Dtos;

public record DashboardDto(
    int ActiveSubscriptions,
    int LateMembers,
    int OutstandingDuesCents);
