namespace Qserve.Audio.Models;

public sealed class AudioLibraryPaths
{
    public required string Root { get; init; }
    public string BellFolder => Path.Combine(Root, "Bell");
    public string DigitsFolder => Path.Combine(Root, "Digits");
    public string PhrasesFolder => Path.Combine(Root, "Phrases");

    public string BellPath => Path.Combine(BellFolder, "bell.wav");
    public string CustomerPhrasePath => Path.Combine(PhrasesFolder, "customer_number.wav");
    public string CounterPhrasePath => Path.Combine(PhrasesFolder, "counter_number.wav");
    public string DigitPath(char digit) => Path.Combine(DigitsFolder, $"{digit}.wav");
}
