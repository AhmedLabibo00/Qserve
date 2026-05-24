using Qserve.Core.Models;

namespace Qserve.Core.Interfaces;

public interface IKeyMappingRepository
{
    IReadOnlyList<KeyMapping> GetAll();
    KeyMapping? GetByAction(string action);
    KeyMapping? GetByKeyDeviceScope(string key, string device, string scope);
    void Save(KeyMapping mapping);
    void SaveMany(IEnumerable<KeyMapping> mappings);
    void DeleteByAction(string action);
}
