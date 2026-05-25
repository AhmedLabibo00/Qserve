using Qserve.Core.Models;

namespace Qserve.Core.Interfaces;

public interface ISecuritySettingsRepository
{
    SecuritySettings Get();
    void Save(SecuritySettings settings);
}
