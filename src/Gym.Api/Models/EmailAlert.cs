// =============================
// File: Models/EmailAlert.cs
// =============================
namespace Gym.Api.Models;

public enum AlertType
{
    Overdue,
    TokenPushFail,
    KycIssue
}

public class EmailAlert
{
    public long AlertId { get; set; }
    public AlertType AlertType { get; set; }
    public long RelatedMember { get; set; }
    public string? Details { get; set; }
    public DateTime SentAt { get; set; }
}
