namespace Gym.Client.Models;

public record DashboardDto(
    int ActiveSubscriptions,
    int LateMembers,
    int OutstandingDuesCents);
