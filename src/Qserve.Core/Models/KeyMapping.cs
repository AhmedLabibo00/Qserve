namespace Qserve.Core.Models;

public sealed class KeyMapping
{
    public long Id { get; init; }
    public required string Action { get; set; }
    public required string Key { get; set; }
    public required string Device { get; set; }
    public required string Scope { get; set; } // Global | PageSpecific | Disabled
    public bool Enabled { get; set; } = true;
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; set; }
}
