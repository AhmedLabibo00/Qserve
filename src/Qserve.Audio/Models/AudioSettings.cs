namespace Qserve.Audio.Models;

public sealed class AudioSettings
{
    public AudioMode Mode { get; init; } = AudioMode.Silent;
    public int BellVolume { get; init; } = 50;
    public int AnnouncementVolume { get; init; } = 50;
    public string? BellFilePath { get; init; }
    public string? AnnouncementFilePath { get; init; }
}
