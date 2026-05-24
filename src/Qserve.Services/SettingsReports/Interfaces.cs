namespace Qserve.Services.SettingsReports;

public interface ISettingsStore
{
    GeneralSettings LoadGeneral();
    ModuleSettings LoadModules();
    void SaveGeneral(GeneralSettings settings);
    void SaveModules(ModuleSettings settings);
}

public interface ISummaryRepository
{
    DailySummary GetDailySummary(DateOnly date);
}

public interface IExportService
{
    byte[] ExportDailySummary(DailySummary summary, ExportFormat format);
}

public interface IArchiveService
{
    string ArchiveDatabase(DateOnly date);
}
