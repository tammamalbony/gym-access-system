// =============================
// File: Models/Fingerprint.cs
// =============================
namespace Gym.Api.Models;

public enum FingerLabel
{
    RIGHT_THUMB,
    RIGHT_INDEX,
    RIGHT_MIDDLE,
    RIGHT_RING,
    RIGHT_LITTLE,
    LEFT_THUMB,
    LEFT_INDEX,
    LEFT_MIDDLE,
    LEFT_RING,
    LEFT_LITTLE
}

public class Fingerprint
{
    public long FingerprintId { get; set; }
    public long MemberId { get; set; }
    public FingerLabel FingerLabel { get; set; }
    public byte[] TemplateBlob { get; set; } = [];
    public DateTime EnrolledAt { get; set; }
    public byte QualityScore { get; set; }
}
