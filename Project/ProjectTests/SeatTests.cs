using Project.Classes;

namespace ProjectTests;

[TestFixture]
public class SeatTests
{
    private Auditorium _auditorium;

    [SetUp]
    public void SetUp()
    {
        _auditorium = new Auditorium(1, "Main Hall");
    }

    [Test]
    public void Constructor_ValidData_CreatesSeat()
    {
        var seat = new Seat(5, 3, Seat.SeatType.VIP, _auditorium);

        Assert.That(seat.Number, Is.EqualTo(5));
        Assert.That(seat.Row, Is.EqualTo(3));
        Assert.That(seat.Type, Is.EqualTo(Seat.SeatType.VIP));
        Assert.That(seat.Auditorium, Is.EqualTo(_auditorium));
    }

    [TestCase(1)]
    [TestCase(10)]
    [TestCase(99)]
    public void SeatNumber_ValidNumber_True(int number)
    {
        var seat = new Seat(number, 3, Seat.SeatType.Standard, _auditorium);
        Assert.That(seat.Number, Is.EqualTo(number));
    }

    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(-50)]
    public void SeatNumber_InvalidNumber_ThrowsArgumentException(int number)
    {
        Assert.That(
            () => new Seat(number, 3, Seat.SeatType.Standard, _auditorium),
            Throws.TypeOf<ArgumentException>()
        );
    }

    [TestCase(1)]
    [TestCase(5)]
    [TestCase(20)]
    public void SeatRow_ValidRow_True(int row)
    {
        var seat = new Seat(5, row, Seat.SeatType.Standard, _auditorium);
        Assert.That(seat.Row, Is.EqualTo(row));
    }

    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(-10)]
    public void SeatRow_InvalidRow_ThrowsArgumentException(int row)
    {
        Assert.That(
            () => new Seat(5, row, Seat.SeatType.Standard, _auditorium),
            Throws.TypeOf<ArgumentException>()
        );
    }

    [TestCase(Seat.SeatType.VIP)]
    [TestCase(Seat.SeatType.Standard)]
    [TestCase(Seat.SeatType.Comfort)]
    public void SeatType_ValidType_True(Seat.SeatType type)
    {
        var seat = new Seat(5, 3, type, _auditorium);
        Assert.That(seat.Type, Is.EqualTo(type));
    }

    [TestCase(-1)]
    [TestCase(999)]
    [TestCase(int.MaxValue)]
    public void SeatType_InvalidType_ThrowsArgumentException(int typeValue)
    {
        Assert.That(
            () => new Seat(5, 3, (Seat.SeatType)typeValue, _auditorium),
            Throws.TypeOf<ArgumentException>()
        );
    }

    [Test]
    public void Constructor_WhenAuditoriumIsNull_ThrowsArgumentException()
    {
        Assert.That(
            () => new Seat(5, 3, Seat.SeatType.VIP, null!),
            Throws.TypeOf<ArgumentException>()
        );
    }

    [Test]
    public void Remove_RemovesSeatFromExtentAndAuditorium()
    {
        var seat = new Seat(5, 3, Seat.SeatType.VIP, _auditorium);

        Assert.That(Seat.Extent.Contains(seat), Is.True);
        Assert.That(_auditorium.Seats.Contains(seat), Is.True);
        
        seat.Remove();

        Assert.That(Seat.Extent.Contains(seat), Is.False);
        Assert.That(_auditorium.Seats.Contains(seat), Is.False);
    }
}