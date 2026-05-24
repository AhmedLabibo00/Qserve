using Qserve.Printing.Models;

namespace Qserve.Printing.Interfaces;

public interface ITicketRenderer
{
    string Render(TicketPrintContent content, PaperSizeOption paperSize);
}
