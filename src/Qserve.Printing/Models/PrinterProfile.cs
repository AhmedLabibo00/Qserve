namespace Qserve.Printing.Models;

public sealed class PrinterProfile
{
    public required string PrinterName { get; init; }
    public required PaperSizeOption PaperSize { get; init; }
}
