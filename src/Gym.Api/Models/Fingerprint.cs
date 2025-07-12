// =============================
// File: Models/Fingerprint.cs
// =============================
namespace Gym.Api.Models;

public enum FingerLabel
{
    RightThumb,
    RightIndex,
    RightMiddle,
    RightRing,
    RightLittle,
    LeftThumb,
    LeftIndex,
    LeftMiddle,
    LeftRing,
    LeftLittle
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
