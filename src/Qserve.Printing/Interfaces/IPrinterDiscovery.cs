namespace Qserve.Printing.Interfaces;

public interface IPrinterDiscovery
{
    IReadOnlyList<string> GetInstalledPrinters();
}
