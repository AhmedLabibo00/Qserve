namespace Qserve.Audio.Models;

public sealed class AnnouncementComposeResult
{
    public List<string> AudioFragments { get; } = new();
    public List<string> Warnings { get; } = new();
}
