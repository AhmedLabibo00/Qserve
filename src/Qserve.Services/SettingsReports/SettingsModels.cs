namespace Qserve.Services.SettingsReports;

public sealed class GeneralSettings
{
    public LanguageOption Language { get; set; } = LanguageOption.English;
    public AppearanceOption Appearance { get; set; } = AppearanceOption.LightBlueWhite;
    public QueueResetMode QueueResetMode { get; set; } = QueueResetMode.Daily;
    public bool StartNewDayOnOpen { get; set; }
}

public sealed class ModuleSettings
{
    public List<int> EnabledServices { get; init; } = new();
    public List<int> EnabledCounters { get; init; } = new();
    public bool AudioEnabled { get; set; } = true;
    public bool PrintingEnabled { get; set; } = true;
    public bool DisplaysEnabled { get; set; } = true;
}

public sealed class AboutInfo
{
    public required string ProductName { get; init; }
    public required string Version { get; init; }
    public required string Manufacturer { get; init; }
    public required string Contact1 { get; init; }
    public required string Contact2 { get; init; }
    public required string Email { get; init; }
}
