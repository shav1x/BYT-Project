namespace Project.Classes.AudioFormat;

public enum DolbyType
{
    TrueHD,
    Digital,
    Atmos
}

public class Dolby(DolbyType type) : IAudioFormat
{
    public DolbyType Type { get; private set; } = type;
}