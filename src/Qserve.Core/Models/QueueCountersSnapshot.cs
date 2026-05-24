namespace Qserve.Core.Models;

public sealed class QueueCountersSnapshot
{
    public required int ServiceId { get; init; }
    public required string ServiceCode { get; init; }
    public required string ServiceNameAr { get; init; }
    public required string ServiceNameEn { get; init; }
    public required int CurrentNumber { get; init; }
    public required int WaitingCount { get; init; }
}
