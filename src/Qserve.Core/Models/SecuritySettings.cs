namespace Qserve.Core.Models;

public sealed class SecuritySettings
{
    public int Id { get; init; } = 1;
    public required string SettingsPassword { get; set; }
    public bool PasswordEnabled { get; set; } = true;
    public DateTime LastChanged { get; set; } = DateTime.UtcNow;
    public int FailedAttempts { get; set; }
}
