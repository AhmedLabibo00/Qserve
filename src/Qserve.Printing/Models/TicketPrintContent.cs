namespace Qserve.Printing.Models;

public sealed class TicketPrintContent
{
    public string? LogoPath { get; init; }
    public required string FacilityName { get; init; }
    public required string QueueNumber { get; init; }
    public required string CurrentNumber { get; init; }
    public required string WaitingCount { get; init; }
    public required string TicketDate { get; init; }
    public required string TicketTime { get; init; }
    public required string Address { get; init; }
    public required string HeaderText { get; init; }
    public required string FooterText { get; init; }
}
