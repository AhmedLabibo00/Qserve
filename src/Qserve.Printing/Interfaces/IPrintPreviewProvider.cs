namespace Qserve.Printing.Interfaces;

public interface IPrintPreviewProvider
{
    string GeneratePreview(string renderedTicket);
}
