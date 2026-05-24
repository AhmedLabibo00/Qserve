using Qserve.Core.Enums;

namespace Qserve.Core.Models;

public sealed class QueueTicket
{
    public long TicketId { get; init; }
    public string TicketNumber { get; set; } = string.Empty;
    public int ServiceId { get; set; }
    public int? CounterId { get; set; }
    public TicketStatus Status { get; set; } = TicketStatus.Waiting;
    public DateOnly QueueDate { get; set; }
    public int QueueSequence { get; set; }
    public DateTime CreatedAtUtc { get; set; }
}
