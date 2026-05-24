namespace Qserve.Audio.Interfaces;

public interface IAudioPlayer
{
    void Play(string filePath, int volumePercent);
}
