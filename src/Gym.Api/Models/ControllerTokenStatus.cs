// =============================
// File: Models/ControllerTokenStatus.cs
// =============================
namespace Gym.Api.Models;

public enum PushStatus
{
    Pending,
    Success,
    Fail
}

public class ControllerTokenStatus
{
    public int ControllerId { get; set; }
    public long TokenId { get; set; }
    public DateTime PushedAt { get; set; }
    public PushStatus PushStatus { get; set; }
}
