using Qserve.Core.Models;

namespace Qserve.Printing.Interfaces;

public interface ITicketPrinter
{
    void Print(QueueTicket ticket);
}
