namespace Qserve.Audio.Models;

public sealed class AnnouncementTestInput
{
    public required string QueueNumber { get; init; }
    public required string CounterNumber { get; init; }
}
