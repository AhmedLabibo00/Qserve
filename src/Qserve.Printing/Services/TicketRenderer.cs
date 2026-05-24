using System.Text;
using Qserve.Printing.Interfaces;
using Qserve.Printing.Models;

namespace Qserve.Printing.Services;

public sealed class TicketRenderer : ITicketRenderer
{
    public string Render(TicketPrintContent content, PaperSizeOption paperSize)
    {
        ArgumentNullException.ThrowIfNull(content);

        var sb = new StringBuilder();
        sb.AppendLine($"[PaperSize: {paperSize}]");

        if (!string.IsNullOrWhiteSpace(content.LogoPath))
            sb.AppendLine($"[Logo: {content.LogoPath}]");

        sb.AppendLine(content.HeaderText);
        sb.AppendLine($"Facility: {content.FacilityName}");
        sb.AppendLine($"Queue: {content.QueueNumber}");
        sb.AppendLine($"Current: {content.CurrentNumber}");
        sb.AppendLine($"Waiting: {content.WaitingCount}");
        sb.AppendLine($"Date: {content.TicketDate}");
        sb.AppendLine($"Time: {content.TicketTime}");
        sb.AppendLine($"Address: {content.Address}");
        sb.AppendLine(content.FooterText);

        return sb.ToString();
    }
}
