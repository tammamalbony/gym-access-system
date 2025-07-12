namespace Gym.Api.Dtos;

public record DashboardDto(
    int ActiveSubscriptions,
    int LateMembers,
    int OutstandingDuesCents);
