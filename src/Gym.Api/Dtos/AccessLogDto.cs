namespace Gym.Api.Dtos;

public record AccessLogDto(
    long Id,
    int ControllerId,
    long MemberId,
    string EventType,
    DateTime EventTime,
    string? Reason);
