using System.Text.Json;
using Qserve.Core.Interfaces;
using Qserve.Core.Models;

namespace Qserve.Services.SettingsReports;

public sealed class KeyMappingService(IKeyMappingRepository repository)
{
    private static readonly Dictionary<string, KeyMappingAssignment> FactoryDefaults = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Toggle Full Screen"] = new() { Action = "Toggle Full Screen", Key = "F11", Device = "Keyboard", Scope = KeyMappingScope.Global },
        ["Next Queue"] = new() { Action = "Next Queue", Key = "F", Device = "Keyboard", Scope = KeyMappingScope.Global },
        ["Previous Queue"] = new() { Action = "Previous Queue", Key = "B", Device = "Keyboard", Scope = KeyMappingScope.Global },
        ["Recall Queue"] = new() { Action = "Recall Queue", Key = "R", Device = "Keyboard", Scope = KeyMappingScope.Global },
        ["Transfer Queue"] = new() { Action = "Transfer Queue", Key = "T", Device = "Keyboard", Scope = KeyMappingScope.Global },
        ["Finish Queue"] = new() { Action = "Finish Queue", Key = "Enter", Device = "Keyboard", Scope = KeyMappingScope.Global },
        ["Open Settings"] = new() { Action = "Open Settings", Key = "F10", Device = "Keyboard", Scope = KeyMappingScope.Global },
        ["Service 1"] = new() { Action = "Service 1", Key = "1", Device = "Keyboard", Scope = KeyMappingScope.PageSpecific },
        ["Service 2"] = new() { Action = "Service 2", Key = "2", Device = "Keyboard", Scope = KeyMappingScope.PageSpecific },
        ["Service 3"] = new() { Action = "Service 3", Key = "3", Device = "Keyboard", Scope = KeyMappingScope.PageSpecific },
        ["Service 4"] = new() { Action = "Service 4", Key = "4", Device = "Keyboard", Scope = KeyMappingScope.PageSpecific },
        ["Service 5"] = new() { Action = "Service 5", Key = "5", Device = "Keyboard", Scope = KeyMappingScope.PageSpecific },
        ["Service 6"] = new() { Action = "Service 6", Key = "6", Device = "Keyboard", Scope = KeyMappingScope.PageSpecific },
        ["Service 7"] = new() { Action = "Service 7", Key = "7", Device = "Keyboard", Scope = KeyMappingScope.PageSpecific },
        ["Service 8"] = new() { Action = "Service 8", Key = "8", Device = "Keyboard", Scope = KeyMappingScope.PageSpecific },
        ["Service 9"] = new() { Action = "Service 9", Key = "9", Device = "Keyboard", Scope = KeyMappingScope.PageSpecific },
    };

    public IReadOnlyList<KeyMapping> GetMappings() => repository.GetAll();

    public KeyMappingConflict ValidateAssignment(KeyMappingAssignment assignment)
    {
        var existing = repository.GetByKeyDeviceScope(assignment.Key, assignment.Device, assignment.Scope.ToString());
        if (existing is null || string.Equals(existing.Action, assignment.Action, StringComparison.OrdinalIgnoreCase))
            return new KeyMappingConflict { HasConflict = false };

        return new KeyMappingConflict
        {
            HasConflict = true,
            Message = "This key is already assigned. Choose another key."
        };
    }

    public void SaveAssignment(KeyMappingAssignment assignment)
    {
        var conflict = ValidateAssignment(assignment);
        if (conflict.HasConflict)
            throw new InvalidOperationException(conflict.Message);

        var existing = repository.GetByAction(assignment.Action);
        var model = new KeyMapping
        {
            Id = existing?.Id ?? 0,
            Action = assignment.Action,
            Key = assignment.Key,
            Device = assignment.Device,
            Scope = assignment.Scope.ToString(),
            Enabled = assignment.Enabled,
            CreatedAt = existing?.CreatedAt ?? DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        repository.Save(model);
    }

    public string ExportJson()
    {
        var mappings = repository.GetAll();
        return JsonSerializer.Serialize(mappings, new JsonSerializerOptions { WriteIndented = true });
    }

    public void ImportJson(string json)
    {
        var mappings = JsonSerializer.Deserialize<List<KeyMapping>>(json) ?? [];
        repository.SaveMany(mappings);
    }

    public void ResetAllToFactoryDefaults()
    {
        var mappings = FactoryDefaults.Values.Select(v => new KeyMapping
        {
            Action = v.Action,
            Key = v.Key,
            Device = v.Device,
            Scope = v.Scope.ToString(),
            Enabled = v.Enabled,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        repository.SaveMany(mappings);
    }
}
