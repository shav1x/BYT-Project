using Project.Classes;
using Project.Classes.AudioFormat;

namespace Project.Classes.PictureFormat;

public class TwoD : ScreeningProfile
{
    private string _aspectRatio = null!;

    public string AspectRatio
    {
        get => _aspectRatio;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Aspect ratio must not be empty.");
            _aspectRatio = value;
        }
    }

    public TwoD(ResolutionType resolution, int framerate, DolbyType dolbyType, string aspectRatio) 
        : base(resolution, framerate, new Dolby(dolbyType))
    {
        AspectRatio = aspectRatio;
        // Set the ScreeningProfile reference after base constructor
        ((Dolby)AudioFormat).ScreeningProfile = this;
    }
    
    public TwoD(ResolutionType resolution, int framerate, string stereoCodec, string aspectRatio) 
        : base(resolution, framerate, new Stereo(stereoCodec))
    {
        AspectRatio = aspectRatio;
        // Set the ScreeningProfile reference after base constructor
        ((Stereo)AudioFormat).ScreeningProfile = this;
    }

    public override bool GlassesRequired => false;
}
