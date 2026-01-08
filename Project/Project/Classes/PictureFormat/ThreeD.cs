using Project.Classes;
using Project.Classes.AudioFormat;

namespace Project.Classes.PictureFormat;

public class ThreeD : ScreeningProfile
{
    public bool? Polarized { get; private set; }

    public ThreeD(ResolutionType resolution, int framerate, DolbyType dolbyType, bool? polarized = null) 
        : base(resolution, framerate, new Dolby(dolbyType))
    {
        Polarized = polarized;
        // Set the ScreeningProfile reference after base constructor
        ((Dolby)AudioFormat).ScreeningProfile = this;
    }
    
    public ThreeD(ResolutionType resolution, int framerate, string stereoCodec, bool? polarized = null) 
        : base(resolution, framerate, new Stereo(stereoCodec))
    {
        Polarized = polarized;
        // Set the ScreeningProfile reference after base constructor
        ((Stereo)AudioFormat).ScreeningProfile = this;
    }

    public override bool GlassesRequired => true;
}