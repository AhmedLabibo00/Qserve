namespace Qserve.Services.SettingsReports;

public sealed class DailySummary
{
    public DateOnly Date { get; init; }
    public int Customers { get; init; }
    public int Services { get; init; }
    public double AverageWaitingMinutes { get; init; }
    public int Finished { get; init; }
    public int Transfer { get; init; }
    public int Cancelled { get; init; }
}
