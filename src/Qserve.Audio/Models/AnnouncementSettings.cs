namespace Qserve.Audio.Models;

public sealed class AnnouncementSettings
{
    public bool EnableAnnouncement { get; init; } = true;
    public bool EnableBell { get; init; } = true;
    public bool EnableCustomerNumber { get; init; } = true;
    public bool EnableCounterNumber { get; init; } = true;
    public int BellVolume { get; init; } = 50;
    public int AnnouncementVolume { get; init; } = 50;
    public AnnouncementMode Mode { get; init; } = AnnouncementMode.BellCustomerCounter;
}
