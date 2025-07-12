// -----------------------------
// File: Models/AlertOptions.cs
// -----------------------------
namespace Gym.Api.Models;

public class AlertOptions
{
    public int GracePromptDays { get; set; } = 0;
    public int SoonExpireDays { get; set; } = 3;
    public List<long> ExemptMembers { get; set; } = new();
}
