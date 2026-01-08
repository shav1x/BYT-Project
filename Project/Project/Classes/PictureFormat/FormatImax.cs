using Project.Classes;
using Project.Classes.AudioFormat;

namespace Project.Classes.PictureFormat;

public class FormatImax : ScreeningProfile
{
    public bool? Laser { get; private set; }

    public FormatImax(ResolutionType resolution, int framerate, DolbyType dolbyType, bool? laser = null) 
        : base(resolution, framerate, new Dolby(dolbyType))
    {
        Laser = laser;
        // Set the ScreeningProfile reference after base constructor
        ((Dolby)AudioFormat).ScreeningProfile = this;
    }
    
    public FormatImax(ResolutionType resolution, int framerate, string stereoCodec, bool? laser = null) 
        : base(resolution, framerate, new Stereo(stereoCodec))
    {
        Laser = laser;
        // Set the ScreeningProfile reference after base constructor
        ((Stereo)AudioFormat).ScreeningProfile = this;
    }

    public override bool GlassesRequired => true;
}