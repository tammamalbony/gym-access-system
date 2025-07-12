// =============================
// File: Models/AccessLog.cs
// =============================
namespace Gym.Api.Models;

public enum AccessEventType
{
    Grant,
    Deny
}

public class AccessLog
{
    public long LogId { get; set; }
    public int ControllerId { get; set; }
    public long MemberId { get; set; }
    public AccessEventType EventType { get; set; }
    public DateTime EventTime { get; set; }
    public string? Reason { get; set; }
}
