// =============================
// File: Models/Controller.cs
// =============================
namespace Gym.Api.Models;

public class Controller
{
    public int ControllerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string IpAddress { get; set; } = string.Empty;
    public string? FirmwareVersion { get; set; }
    public DateTime? LastSeen { get; set; }
}
