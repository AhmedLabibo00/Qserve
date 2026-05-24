namespace Qserve.Services.SettingsReports;

public sealed class ReportsService(
    ISummaryRepository summaryRepository,
    IExportService exportService,
    IArchiveService archiveService)
{
    public DailySummary GetDailySummary(DateOnly date)
    {
        var summary = summaryRepository.GetDailySummary(date);
        Validate(summary);
        return summary;
    }

    public byte[] Export(DailySummary summary, ExportFormat format)
    {
        Validate(summary);
        return exportService.ExportDailySummary(summary, format);
    }

    public string StartNewDayAndArchive(DateOnly date)
    {
        return archiveServiceArchive(date);
    }

    private string archiveServiceArchive(DateOnly date)
    {
        if (date == default)
            throw new InvalidOperationException("Date is required for archive.");

        return archiveService.ArchiveDatabase(date);
    }

    private static void Validate(DailySummary summary)
    {
        ArgumentNullException.ThrowIfNull(summary);
        if (summary.Customers < 0 || summary.Services < 0 || summary.Finished < 0 || summary.Transfer < 0 || summary.Cancelled < 0)
            throw new InvalidOperationException("Summary values cannot be negative.");
        if (summary.AverageWaitingMinutes < 0)
            throw new InvalidOperationException("Average waiting cannot be negative.");
    }
}
