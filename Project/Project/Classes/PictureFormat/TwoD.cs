namespace Project.Classes.PictureFormat;

public class TwoD : IPictureFormat
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

    public TwoD(string aspectRatio)
    {
        AspectRatio = aspectRatio;
    }
}
