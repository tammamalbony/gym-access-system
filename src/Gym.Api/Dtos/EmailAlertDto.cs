namespace Gym.Api.Dtos;

public record EmailAlertDto(
    long Id,
    string AlertType,
    long MemberId,
    string? Details,
    DateTime SentAt);
