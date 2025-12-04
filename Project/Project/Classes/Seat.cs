namespace Project.Classes;

public class Seat
{
    public enum SeatType
    {
        VIP,
        Standard,
        Comfort
    }
    
    private static readonly List<Seat> _extent = new();
    public static IReadOnlyList<Seat> Extent => _extent.AsReadOnly();
    
    private int _number;
    private int _row;
    private SeatType _type;
    
    private Auditorium? _auditorium;
    public Auditorium Auditorium
    {
        get => _auditorium ?? throw new InvalidOperationException("Seat must be associated with an Auditorium.");
        private set
        {
            if (value == null)
                throw new ArgumentException("A Seat cannot exist without an Auditorium.");
            _auditorium = value;
        }
    }

    public int Number
    {
        get => _number;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Seat number must be greater than 0.");
            _number = value;
        }
    }

    public int Row
    {
        get => _row;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Row must be greater than 0.");
            _row = value;
        }
    }
    public SeatType Type
    {
        get => _type;
        set
        {
            if (!Enum.IsDefined(typeof(SeatType), value))
                throw new ArgumentException("Invalid seat type specified.");
            _type = value;
        }
    }
    
    public Seat(int number, int row, SeatType type, Auditorium auditorium)
    {
        Number = number;
        Row = row;
        Type = type;
        Auditorium = auditorium;
        _extent.Add(this);
        auditorium.AddSeat(this);
    }
    
    public static void LoadExtent(List<Seat>? seats)
    {
        _extent.Clear();

        if (seats is null || seats.Count == 0)
            return;

        _extent.AddRange(seats);
    }
    
    public void Remove()
    {
        if (_auditorium != null)
        {
            _auditorium.RemoveSeat(this);
            _auditorium = null;
            _extent.Remove(this);
        }
    }

    ~Seat()
    {
        Remove();
    }
    
}
