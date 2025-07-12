// =============================
// File: Models/Payment.cs
// =============================
namespace Gym.Api.Models;

public enum PaymentMethod
{
    Cash,
    Card,
    Online
}

public class Payment
{
    public long PaymentId { get; set; }
    public long SubscriptionId { get; set; }
    public int AmountCents { get; set; }
    public DateOnly PaidOn { get; set; }
    public PaymentMethod Method { get; set; }
    public string? TxnReference { get; set; }
    public string? RecordedBy { get; set; }
    public DateTime CreatedAt { get; set; }
}
