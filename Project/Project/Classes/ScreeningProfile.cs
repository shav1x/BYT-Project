using Project.Classes.AudioFormat;

namespace Project.Classes;

public abstract class ScreeningProfile
{
    private static readonly List<ScreeningProfile> _extent = new();
    public static IReadOnlyList<ScreeningProfile> Extent => _extent.AsReadOnly();
    
    public enum ResolutionType
    {
        HD,
        FullHD,
        _2K,
        _4K,
        _8K
    }

    private ResolutionType _resolution;
    private int _framerate;
    
    private Auditorium? _auditorium; 
    public Auditorium? Auditorium => _auditorium;

    private object? _audioFormat;
    public object AudioFormat
    {
        get => _audioFormat ?? throw new InvalidOperationException("ScreeningProfile must have an AudioFormat.");
        private set
        {
            if (value == null)
                throw new ArgumentException("Audio format cannot be null.");
            _audioFormat = value;
        }
    }

    public ResolutionType Resolution
    {
        get => _resolution;
        set
        {
            if (!Enum.IsDefined(typeof(ResolutionType), value))
                throw new ArgumentException("Invalid resolution specified.");
            _resolution = value;
        }
    }
    
    public int Framerate
    {
        get => _framerate;
        set
        {
            if (value < 0)
                throw new ArgumentException("Invalid framerate specified.");
            _framerate = value;
        }
    }
    
    public abstract bool GlassesRequired { get; }

    protected ScreeningProfile(ResolutionType resolution, int framerate, object audioFormat)
    {
        _resolution = resolution;
        _framerate = framerate;
        AudioFormat = audioFormat ?? throw new ArgumentNullException(nameof(audioFormat), "Audio format cannot be null.");
        _extent.Add(this);
    }
    
    public static void LoadExtent(List<ScreeningProfile>? screeningProfiles)
    {
        _extent.Clear();

        if (screeningProfiles is null || screeningProfiles.Count == 0)
            return;

        _extent.AddRange(screeningProfiles);
    }
    
    public void SetAuditorium(Auditorium? auditorium)
    {
        if (_auditorium == auditorium)
            return;

        if (auditorium == null)
        {
            _auditorium = null;
            return;
        }
        
        if (_auditorium != null && _auditorium != auditorium)
            throw new InvalidOperationException(
                "This ScreeningProfile is already assigned to another Auditorium.");

        _auditorium = auditorium;

        if (auditorium.ScreeningProfile != this)
            auditorium.SetScreeningProfile(this);
    }
    
    public void RemoveAudioFormat()
    {
        if (_audioFormat != null)
        {
            if (_audioFormat is Dolby dolby)
            {
                _audioFormat = null;
                dolby.Remove();
            }
            else if (_audioFormat is Stereo stereo)
            {
                _audioFormat = null;
                stereo.Remove();
            }
        }
    }
    
    public void Remove()
    {
        if (_auditorium != null)
        {
            _auditorium.SetScreeningProfile(null);
            _auditorium = null;
        }
        
        // Remove audio format (composition - audio format cannot exist without screening profile)
        RemoveAudioFormat();
        
        _extent.Remove(this);
    }
    
    ~ScreeningProfile()
    {
        Remove();
    }
}
