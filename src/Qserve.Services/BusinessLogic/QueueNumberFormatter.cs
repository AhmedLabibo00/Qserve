namespace Qserve.Services.BusinessLogic;

public static class QueueNumberFormatter
{
    public static string Format(string servicePrefix, int sequence)
    {
        if (string.IsNullOrWhiteSpace(servicePrefix))
            throw new ArgumentException("Service prefix is required.", nameof(servicePrefix));

        if (sequence <= 0)
            throw new ArgumentOutOfRangeException(nameof(sequence), "Sequence must be greater than zero.");

        return $"{servicePrefix.Trim().ToUpperInvariant()}-{sequence:0000}";
    }
}
