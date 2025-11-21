namespace Project.Classes;

public class Seat
{
    public enum SeatType
    {
        VIP,
        Standard,
        Comfort
    }
    
    private int _number;
    private int _row;
    private SeatType _type;

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
    
    public Seat(int number, int row, SeatType type)
    {
        Number = number;
        Row = row;
        Type = type;
    }
}