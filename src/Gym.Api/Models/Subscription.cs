// =============================
// File: Models/Subscription.cs
// =============================
namespace Gym.Api.Models;

public enum SubscriptionStatus
{
    Active,
    Expired,
    Cancelled
}

public class Subscription
{
    public long SubscriptionId { get; set; }
    public long MemberId { get; set; }
    public int PlanId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public SubscriptionStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
