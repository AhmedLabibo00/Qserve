namespace Qserve.Services.SettingsReports;

public sealed class SettingsService(ISettingsStore settingsStore)
{
    public GeneralSettings GetGeneralSettings() => settingsStore.LoadGeneral();
    public ModuleSettings GetModuleSettings() => settingsStore.LoadModules();

    public void UpdateLanguage(LanguageOption language)
    {
        var current = settingsStore.LoadGeneral();
        current.Language = language;
        settingsStore.SaveGeneral(current);
    }

    public void UpdateAppearance(AppearanceOption appearance)
    {
        var current = settingsStore.LoadGeneral();
        current.Appearance = appearance;
        settingsStore.SaveGeneral(current);
    }

    public void UpdateQueueResetMode(QueueResetMode mode, bool startNewDay)
    {
        var current = settingsStore.LoadGeneral();
        current.QueueResetMode = mode;
        current.StartNewDayOnOpen = startNewDay;
        settingsStore.SaveGeneral(current);
    }

    public void UpdateModules(ModuleSettings moduleSettings)
    {
        Validate(moduleSettings);
        settingsStore.SaveModules(moduleSettings);
    }

    private static void Validate(ModuleSettings moduleSettings)
    {
        ArgumentNullException.ThrowIfNull(moduleSettings);
        if (moduleSettings.EnabledServices.Count == 0)
            throw new InvalidOperationException("At least one service must be enabled.");
        if (moduleSettings.EnabledCounters.Count == 0)
            throw new InvalidOperationException("At least one counter must be enabled.");
    }
}
