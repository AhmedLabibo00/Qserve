namespace Qserve.Core.Models;

public sealed class InputDeviceInfo
{
    public required string DeviceName { get; init; }
    public required string DeviceType { get; init; }
    public bool Connected { get; init; }
}
