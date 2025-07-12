namespace Gym.Client.Models;

public record AccessLogDto(
    long Id,
    int ControllerId,
    long MemberId,
    string EventType,
    DateTime EventTime,
    string? Reason);
