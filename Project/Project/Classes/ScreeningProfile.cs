using Project.Classes.AudioFormat;
using Project.Classes.PictureFormat;

namespace Project.Classes;

public class ScreeningProfile
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
    public IAudioFormat AudioFormat { get; private set; }
    public IPictureFormat PictureFormat { get; private set; }

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
    
    public bool GlassesRequired => PictureFormat is ThreeD or FormatImax;

    public ScreeningProfile(ResolutionType resolution, int framerate, IAudioFormat audioFormat, IPictureFormat pictureFormat)
    {
        _resolution = resolution;
        _framerate = framerate;
        AudioFormat = audioFormat ?? throw new ArgumentNullException(nameof(audioFormat), "Audio format cannot be null.");
        PictureFormat = pictureFormat ?? throw new ArgumentNullException(nameof(pictureFormat), "Picture format cannot be null.");
        _extent.Add(this);
    }
    
    public static void LoadExtent(List<ScreeningProfile>? screeningProfiles)
    {
        _extent.Clear();

        if (screeningProfiles is null || screeningProfiles.Count == 0)
            return;

        _extent.AddRange(screeningProfiles);
    }
    
}
