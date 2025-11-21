using Project.Classes;

namespace ProjectTests;

[TestFixture]
public class SeatTests
{
    [Test]
    public void Constructor_ValidData_CreatesSeat()
    {
        var seat = new Seat(5, 3, Seat.SeatType.VIP);

        Assert.That(seat.Number, Is.EqualTo(5));
        Assert.That(seat.Row, Is.EqualTo(3));
        Assert.That(seat.Type, Is.EqualTo(Seat.SeatType.VIP));
    }

    [TestCase(1)]
    [TestCase(10)]
    [TestCase(99)]
    public void SeatNumber_ValidNumber_True(int number)
    {
        var seat = new Seat(number, 3, Seat.SeatType.Standard);
        Assert.That(seat.Number, Is.EqualTo(number));
    }

    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(-50)]
    public void SeatNumber_InvalidNumber_ThrowsArgumentException(int number)
    {
        Assert.That(
            () => new Seat(number, 3, Seat.SeatType.Standard),
            Throws.TypeOf<ArgumentException>()
        );
    }

    [TestCase(1)]
    [TestCase(5)]
    [TestCase(20)]
    public void SeatRow_ValidRow_True(int row)
    {
        var seat = new Seat(5, row, Seat.SeatType.Standard);
        Assert.That(seat.Row, Is.EqualTo(row));
    }

    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(-10)]
    public void SeatRow_InvalidRow_ThrowsArgumentException(int row)
    {
        Assert.That(
            () => new Seat(5, row, Seat.SeatType.Standard),
            Throws.TypeOf<ArgumentException>()
        );
    }

    [TestCase(Seat.SeatType.VIP)]
    [TestCase(Seat.SeatType.Standard)]
    [TestCase(Seat.SeatType.Comfort)]
    public void SeatType_ValidType_True(Seat.SeatType type)
    {
        var seat = new Seat(5, 3, type);
        Assert.That(seat.Type, Is.EqualTo(type));
    }

    [TestCase(-1)]
    [TestCase(999)]
    [TestCase(int.MaxValue)]
    public void SeatType_InvalidType_ThrowsArgumentException(int typeValue)
    {
        Assert.That(
            () => new Seat(5, 3, (Seat.SeatType)typeValue),
            Throws.TypeOf<ArgumentException>()
        );
    }
}