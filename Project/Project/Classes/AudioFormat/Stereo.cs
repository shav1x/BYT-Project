using Project.Classes;

namespace Project.Classes.AudioFormat;

public class Stereo
{
    private static readonly List<Stereo> _extent = new();
    public static IReadOnlyList<Stereo> Extent => _extent.AsReadOnly();
    
    private ScreeningProfile? _screeningProfile;
    public ScreeningProfile ScreeningProfile
    {
        get => _screeningProfile ?? throw new InvalidOperationException("Stereo must be associated with a ScreeningProfile.");
        internal set
        {
            if (value == null)
                throw new ArgumentException("A Stereo cannot exist without a ScreeningProfile.");
            _screeningProfile = value;
        }
    }
    
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

    internal Stereo(string codec)
    {
        AudioCodec = codec;
        _extent.Add(this);
    }

    public static Stereo Create(ScreeningProfile screeningProfile, string codec)
    {
        if (screeningProfile == null)
            throw new ArgumentNullException(nameof(screeningProfile));
        
        var stereo = new Stereo(codec);
        stereo.ScreeningProfile = screeningProfile;
        return stereo;
    }
    
    public static void LoadExtent(List<Stereo>? stereos)
    {
        _extent.Clear();

        if (stereos is null || stereos.Count == 0)
            return;

        _extent.AddRange(stereos);
    }
    
    public void Remove()
    {
        if (_screeningProfile != null)
        {
            _screeningProfile.RemoveAudioFormat();
        }
        _screeningProfile = null!;
        _extent.Remove(this);
    }
    
    ~Stereo()
    {
        Remove();
    }
}