namespace Qserve.Audio.Library;

public sealed class AudioFragmentCache
{
    private readonly Dictionary<string, string> _cache = new(StringComparer.OrdinalIgnoreCase);

    public void Preload(IEnumerable<string> paths)
    {
        foreach (var path in paths)
        {
            if (File.Exists(path))
                _cache[path] = path;
        }
    }

    public bool TryGet(string path, out string cachedPath)
    {
        if (_cache.TryGetValue(path, out cachedPath!))
            return true;

        if (File.Exists(path))
        {
            _cache[path] = path;
            cachedPath = path;
            return true;
        }

        cachedPath = string.Empty;
        return false;
    }
}
