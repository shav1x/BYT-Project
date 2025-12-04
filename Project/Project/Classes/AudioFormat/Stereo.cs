namespace Project.Classes.AudioFormat;

public class Stereo : IAudioFormat
{
    private string _audioCodec = null!;

    public string AudioCodec
    {
        get => _audioCodec;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Audio codec must not be empty.");
            _audioCodec = value;
        }
    }

    public Stereo(string codec)
    {
        AudioCodec = codec;
    }
}