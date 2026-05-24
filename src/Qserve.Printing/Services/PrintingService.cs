using Qserve.Printing.Interfaces;
using Qserve.Printing.Models;

namespace Qserve.Printing.Services;

public sealed class PrintingService(
    IPrinterDiscovery printerDiscovery,
    ITicketRenderer ticketRenderer,
    IPrintPreviewProvider previewProvider)
{
    public IReadOnlyList<string> GetAvailablePrinters() => printerDiscovery.GetInstalledPrinters();

    public string BuildPreview(TicketPrintContent content, PrinterProfile profile)
    {
        Validate(content, profile);
        var rendered = ticketRenderer.Render(content, profile.PaperSize);
        return previewProvider.GeneratePreview(rendered);
    }

    public string BuildPrintPayload(TicketPrintContent content, PrinterProfile profile)
    {
        Validate(content, profile);
        return ticketRenderer.Render(content, profile.PaperSize);
    }

    private static void Validate(TicketPrintContent content, PrinterProfile profile)
    {
        ArgumentNullException.ThrowIfNull(content);
        ArgumentNullException.ThrowIfNull(profile);

        if (string.IsNullOrWhiteSpace(profile.PrinterName))
            throw new InvalidOperationException("Printer selection is required.");

        if (string.IsNullOrWhiteSpace(content.FacilityName))
            throw new InvalidOperationException("Facility is required.");

        if (string.IsNullOrWhiteSpace(content.QueueNumber))
            throw new InvalidOperationException("Queue is required.");

        if (string.IsNullOrWhiteSpace(content.FooterText))
            throw new InvalidOperationException("Footer is required.");
    }
}
