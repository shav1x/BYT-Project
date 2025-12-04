using System.Text.RegularExpressions;

namespace Project.Classes;

public class Auditorium
{
    private static readonly List<Auditorium> _extent = new();
    public static IReadOnlyList<Auditorium> Extent => _extent.AsReadOnly();
    
    private int _number;
    private string _name = null!;
    
    private ScreeningProfile _screeningProfile = null!; 
    public ScreeningProfile ScreeningProfile => _screeningProfile;
    
    private readonly List<Seat> _seats = new();
    public IReadOnlyList<Seat> Seats => _seats.AsReadOnly();
    
    public int Number 
    {
        get => _number;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Auditorium number must be greater than 0.");
            _number = value;
        }
    }
    
    public string Name 
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value) ||
                !Regex.IsMatch(value, @"^[A-Za-z ]+$"))
            {
                throw new ArgumentException("Name must contain only letters.");
            }

            _name = value;
        }
    }
    
    protected Auditorium() { }
    
    public Auditorium(int number, 
        string name, 
        ScreeningProfile screeningProfile)
    {
        Number = number;
        Name = name;
        SetScreeningProfile(screeningProfile);
        _extent.Add(this);
    }
    
    public void SetScreeningProfile(ScreeningProfile profile)
    {
        if (profile is null)
            throw new ArgumentNullException(nameof(profile), "Auditorium must have a screening profile (Multiplicity 1).");
        
        if (profile.Auditorium is not null && profile.Auditorium != this)
            throw new InvalidOperationException("The selected Screening Profile is already in use by another Auditorium.");
        
        _screeningProfile.SetAuditorium(null);
        _screeningProfile = profile;
        _screeningProfile.SetAuditorium(this);
    }
    
    public static void LoadExtent(List<Auditorium>? auditoriums)
    {
        _extent.Clear();

        if (auditoriums is null || auditoriums.Count == 0)
            return;

        _extent.AddRange(auditoriums);
    }
    
    public void AddSeat(Seat seat)
    {
        if (!_seats.Contains(seat))
            _seats.Add(seat);
    }

    public void RemoveSeat(Seat seat)
    {
        if (_seats.Contains(seat))
            _seats.Remove(seat);
    }

    public void Remove()
    {
        foreach (var seat in _seats.ToList())
        {
            seat.Remove();
        }
        
        _screeningProfile.SetAuditorium(null);
        
        _extent.Remove(this);
    }

    ~Auditorium()
    {
        Remove();
    }
}
