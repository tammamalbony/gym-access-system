namespace Gym.Core.Dtos;

public record EmailAlertDto(
    long Id,
    string AlertType,
    long MemberId,
    string? Details,
    DateTime SentAt);
