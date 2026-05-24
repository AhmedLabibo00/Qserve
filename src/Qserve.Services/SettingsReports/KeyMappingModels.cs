namespace Qserve.Services.SettingsReports;

public enum KeyMappingScope
{
    Global,
    PageSpecific,
    Disabled
}

public sealed class KeyMappingAssignment
{
    public required string Action { get; init; }
    public required string Key { get; init; }
    public required string Device { get; init; }
    public KeyMappingScope Scope { get; init; } = KeyMappingScope.Global;
    public bool Enabled { get; init; } = true;
}

public sealed class KeyMappingConflict
{
    public required bool HasConflict { get; init; }
    public string? Message { get; init; }
}
