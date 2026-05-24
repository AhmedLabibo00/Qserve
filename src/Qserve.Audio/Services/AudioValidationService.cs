using Qserve.Audio.Models;

namespace Qserve.Audio.Services;

public static class AudioValidationService
{
    public static void ValidateVolume(int volumePercent, string paramName)
    {
        if (volumePercent < 0 || volumePercent > 100)
            throw new ArgumentOutOfRangeException(paramName, "Volume must be between 0 and 100.");
    }

    public static AudioFormat EnsureSupportedFormat(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);
        var ext = Path.GetExtension(filePath).TrimStart('.').ToLowerInvariant();

        return ext switch
        {
            "mp3" => AudioFormat.Mp3,
            "wav" => AudioFormat.Wav,
            "ogg" => AudioFormat.Ogg,
            _ => throw new InvalidOperationException("Unsupported format. Allowed: mp3, wav, ogg.")
        };
    }
}
