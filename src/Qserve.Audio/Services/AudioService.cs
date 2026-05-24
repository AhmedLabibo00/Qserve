using Qserve.Audio.Interfaces;
using Qserve.Audio.Models;

namespace Qserve.Audio.Services;

public sealed class AudioService(IAudioPlayer audioPlayer)
{
    public void TestBell(AudioSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);

        AudioValidationService.ValidateVolume(settings.BellVolume, nameof(settings.BellVolume));

        if (settings.Mode == AudioMode.Silent)
            return;

        if (settings.Mode is AudioMode.Bell or AudioMode.Announcement)
        {
            if (string.IsNullOrWhiteSpace(settings.BellFilePath))
                throw new InvalidOperationException("Bell file is required.");

            AudioValidationService.EnsureSupportedFormat(settings.BellFilePath);
            audioPlayer.Play(settings.BellFilePath, settings.BellVolume);
        }
    }

    public void TestAnnouncement(AudioSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);

        AudioValidationService.ValidateVolume(settings.AnnouncementVolume, nameof(settings.AnnouncementVolume));

        if (settings.Mode == AudioMode.Silent)
            return;

        if (settings.Mode == AudioMode.Announcement)
        {
            if (string.IsNullOrWhiteSpace(settings.AnnouncementFilePath))
                throw new InvalidOperationException("Announcement file is required.");

            AudioValidationService.EnsureSupportedFormat(settings.AnnouncementFilePath);
            audioPlayer.Play(settings.AnnouncementFilePath, settings.AnnouncementVolume);
        }
    }
}
