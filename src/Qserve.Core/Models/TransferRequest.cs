namespace Qserve.Core.Models;

public sealed class TransferRequest
{
    public required long TicketId { get; init; }
    public required int ToServiceId { get; init; }
    public int? ToCounterId { get; init; }
    public required bool KeepNumber { get; init; }
}
