using Project.Classes;

namespace Project.Classes.AudioFormat;

public enum DolbyType
{
    TrueHD,
    Digital,
    Atmos
}

public class Dolby
{
    private static readonly List<Dolby> _extent = new();
    public static IReadOnlyList<Dolby> Extent => _extent.AsReadOnly();
    
    private ScreeningProfile? _screeningProfile;
    public ScreeningProfile ScreeningProfile
    {
        get => _screeningProfile ?? throw new InvalidOperationException("Dolby must be associated with a ScreeningProfile.");
        internal set
        {
            if (value == null)
                throw new ArgumentException("A Dolby cannot exist without a ScreeningProfile.");
            _screeningProfile = value;
        }
    }
    
    public DolbyType Type { get; private set; }

    internal Dolby(DolbyType type)
    {
        Type = type;
        _extent.Add(this);
    }

    public static Dolby Create(ScreeningProfile screeningProfile, DolbyType type)
    {
        if (screeningProfile == null)
            throw new ArgumentNullException(nameof(screeningProfile));
        
        var dolby = new Dolby(type);
        dolby.ScreeningProfile = screeningProfile;
        return dolby;
    }
    
    public static void LoadExtent(List<Dolby>? dolbies)
    {
        _extent.Clear();

        if (dolbies is null || dolbies.Count == 0)
            return;

        _extent.AddRange(dolbies);
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
    
    ~Dolby()
    {
        Remove();
    }
}