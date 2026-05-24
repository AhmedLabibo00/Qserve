using Qserve.Audio.Interfaces;
using Qserve.Audio.Library;
using Qserve.Audio.Models;

namespace Qserve.Audio.Engine;

public sealed class AnnouncementEngine(
    IAudioPlayer audioPlayer,
    AudioFragmentCache cache,
    AnnouncementComposer composer)
{
    public void Preload(AudioLibraryPaths library)
    {
        var preloadPaths = new List<string>
        {
            library.BellPath,
            library.CustomerPhrasePath,
            library.CounterPhrasePath
        };

        for (var i = 0; i <= 9; i++)
            preloadPaths.Add(library.DigitPath(char.Parse(i.ToString())));

        cache.Preload(preloadPaths);
    }

    public AnnouncementComposeResult Preview(AudioLibraryPaths library, AnnouncementSettings settings, AnnouncementTestInput input)
        => composer.Compose(library, settings, input);

    public AnnouncementComposeResult PlayTest(AudioLibraryPaths library, AnnouncementSettings settings, AnnouncementTestInput input)
    {
        AudioValidationService.ValidateVolume(settings.BellVolume, nameof(settings.BellVolume));
        AudioValidationService.ValidateVolume(settings.AnnouncementVolume, nameof(settings.AnnouncementVolume));

        var composed = composer.Compose(library, settings, input);

        foreach (var fragment in composed.AudioFragments)
        {
            AudioValidationService.EnsureSupportedFormat(fragment);
            var vol = fragment.Contains("Bell", StringComparison.OrdinalIgnoreCase) ? settings.BellVolume : settings.AnnouncementVolume;
            audioPlayer.Play(fragment, vol);
        }

        return composed;
    }
}
