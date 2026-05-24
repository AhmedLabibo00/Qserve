using System.Text.RegularExpressions;
using Qserve.Audio.Library;
using Qserve.Audio.Models;

namespace Qserve.Audio.Engine;

public sealed class AnnouncementComposer(AudioFragmentCache cache)
{
    public AnnouncementComposeResult Compose(AudioLibraryPaths library, AnnouncementSettings settings, AnnouncementTestInput input)
    {
        ArgumentNullException.ThrowIfNull(library);
        ArgumentNullException.ThrowIfNull(settings);
        ArgumentNullException.ThrowIfNull(input);

        var result = new AnnouncementComposeResult();

        if (!settings.EnableAnnouncement)
            return result;

        var digits = ExtractDigits(input.QueueNumber);
        var counterDigits = ExtractDigits(input.CounterNumber);

        var withBell = settings.Mode is AnnouncementMode.BellCustomerCounter or AnnouncementMode.BellCustomer or AnnouncementMode.BellOnly or AnnouncementMode.BellCustomerWithoutCounter;
        var withCustomer = settings.Mode is AnnouncementMode.BellCustomerCounter or AnnouncementMode.CustomerCounter or AnnouncementMode.BellCustomer or AnnouncementMode.CustomerOnly or AnnouncementMode.BellCustomerWithoutCounter;
        var withCounter = settings.EnableCounterNumber && settings.Mode is AnnouncementMode.BellCustomerCounter or AnnouncementMode.CustomerCounter;

        if (withBell && settings.EnableBell)
            AddOptional(cache, library.BellPath, result, "Bell audio missing");

        if (withCustomer && settings.EnableCustomerNumber)
        {
            AddOptional(cache, library.CustomerPhrasePath, result, "Customer phrase missing");
            AddDigits(cache, library, digits, result);
        }

        if (withCounter)
        {
            AddOptional(cache, library.CounterPhrasePath, result, "Counter phrase missing");
            AddDigits(cache, library, counterDigits, result);
        }

        return result;
    }

    public static string ExtractDigits(string value) => Regex.Replace(value ?? string.Empty, "[^0-9]", string.Empty);

    private static void AddDigits(AudioFragmentCache cache, AudioLibraryPaths library, string digits, AnnouncementComposeResult result)
    {
        foreach (var d in digits)
        {
            var path = library.DigitPath(d);
            if (cache.TryGet(path, out var fragment))
                result.AudioFragments.Add(fragment);
            else
                result.Warnings.Add($"Missing digit audio: {d}");
        }
    }

    private static void AddOptional(AudioFragmentCache cache, string path, AnnouncementComposeResult result, string warning)
    {
        if (cache.TryGet(path, out var fragment))
            result.AudioFragments.Add(fragment);
        else
            result.Warnings.Add(warning);
    }
}
