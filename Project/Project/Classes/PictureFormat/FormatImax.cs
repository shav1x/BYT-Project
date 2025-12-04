namespace Project.Classes.PictureFormat;

public class FormatImax(bool laser) : IPictureFormat
{
    public bool Laser { get; private set; } = laser;
}