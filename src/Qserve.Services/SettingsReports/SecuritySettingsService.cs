using Qserve.Core.Interfaces;

namespace Qserve.Services.SettingsReports;

public sealed class SecuritySettingsService(ISecuritySettingsRepository repository)
{
    public bool VerifyPassword(string input)
    {
        var settings = repository.Get();
        if (!settings.PasswordEnabled)
            return true;

        if (settings.SettingsPassword == input)
        {
            settings.FailedAttempts = 0;
            repository.Save(settings);
            return true;
        }

        settings.FailedAttempts++;
        repository.Save(settings);
        return false;
    }

    public bool ShouldShowForgotPassword() => repository.Get().FailedAttempts >= 3;

    public void ResetToDefaultPassword()
    {
        var settings = repository.Get();
        settings.SettingsPassword = "0000";
        settings.FailedAttempts = 0;
        settings.LastChanged = DateTime.UtcNow;
        repository.Save(settings);
    }

    public void ChangePassword(string newPassword)
    {
        if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 4 || newPassword.Length > 10 || !newPassword.All(char.IsDigit))
            throw new InvalidOperationException("Password must be 4-10 digits.");

        var settings = repository.Get();
        settings.SettingsPassword = newPassword;
        settings.LastChanged = DateTime.UtcNow;
        settings.FailedAttempts = 0;
        repository.Save(settings);
    }

    public void SetPasswordEnabled(bool enabled)
    {
        var settings = repository.Get();
        settings.PasswordEnabled = enabled;
        repository.Save(settings);
    }
}
