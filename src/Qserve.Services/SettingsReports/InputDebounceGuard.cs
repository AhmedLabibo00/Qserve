namespace Qserve.Services.SettingsReports;

public sealed class InputDebounceGuard
{
    private readonly Dictionary<string, DateTime> _last = new(StringComparer.OrdinalIgnoreCase);
    private readonly TimeSpan _interval = TimeSpan.FromMilliseconds(200);

    public bool CanProcess(string inputToken)
    {
        var now = DateTime.UtcNow;
        if (_last.TryGetValue(inputToken, out var last) && now - last < _interval)
            return false;

        _last[inputToken] = now;
        return true;
    }
}
