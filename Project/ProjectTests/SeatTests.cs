using Project.Classes;
using Project.Classes.AudioFormat;
using Project.Classes.PictureFormat;

namespace ProjectTests;

[TestFixture]
public class SeatTests
{
    private Auditorium _auditorium;

    [SetUp]
    public void SetUp()
    {
        _auditorium = new Auditorium(1, "Main Hall",
            new ScreeningProfile(ScreeningProfile.ResolutionType.FullHD, 60, new Stereo("codec"),
                new FormatImax(true)));
    }

    [Test]
    public void Constructor_ValidData_InitializesSeatCorrectly()
    {
        var seat = CreateSeat(5, 3, Seat.SeatType.VIP);

        Assert.Multiple(() =>
        {
            Assert.That(seat.Number, Is.EqualTo(5));
            Assert.That(seat.Row, Is.EqualTo(3));
            Assert.That(seat.Type, Is.EqualTo(Seat.SeatType.VIP));
            Assert.That(seat.Auditorium, Is.EqualTo(_auditorium));
            Assert.That(Seat.Extent, Does.Contain(seat));
            Assert.That(_auditorium.Seats, Does.Contain(seat));
        });
    }

    [TestCase(1)]
    [TestCase(10)]
    [TestCase(99)]
    public void Constructor_ValidSeatNumber_ShouldSetSeatNumberCorrectly(int number)
    {
        var seat = CreateSeat(number, 3, Seat.SeatType.Standard);

        Assert.That(seat.Number, Is.EqualTo(number));
    }

    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(-50)]
    public void Constructor_InvalidSeatNumber_ThrowsArgumentException(int number)
    {
        Assert.Throws<ArgumentException>(() => CreateSeat(number, 3, Seat.SeatType.Standard));
    }

    [TestCase(1)]
    [TestCase(5)]
    [TestCase(20)]
    public void Constructor_ValidSeatRow_ShouldSetRowCorrectly(int row)
    {
        var seat = CreateSeat(5, row, Seat.SeatType.Standard);

        Assert.That(seat.Row, Is.EqualTo(row));
    }

    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(-10)]
    public void Constructor_InvalidSeatRow_ThrowsArgumentException(int row)
    {
        Assert.Throws<ArgumentException>(() => CreateSeat(5, row, Seat.SeatType.Standard));
    }

    [TestCase(Seat.SeatType.VIP)]
    [TestCase(Seat.SeatType.Standard)]
    [TestCase(Seat.SeatType.Comfort)]
    public void Constructor_ValidSeatType_ShouldSetTypeCorrectly(Seat.SeatType type)
    {
        var seat = CreateSeat(5, 3, type);

        Assert.That(seat.Type, Is.EqualTo(type));
    }

    [TestCase(-1)]
    [TestCase(999)]
    [TestCase(int.MaxValue)]
    public void Constructor_InvalidSeatType_ThrowsArgumentException(int typeValue)
    {
        Assert.Throws<ArgumentException>(() => CreateSeat(5, 3, (Seat.SeatType)typeValue));
    }

    [Test]
    public void Constructor_NullAuditorium_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Seat(5, 3, Seat.SeatType.VIP, null!));
    }

    [Test]
    public void Remove_Seat_RemovesFromExtentAndAuditorium()
    {
        var seat = CreateSeat(5, 3, Seat.SeatType.VIP);

        seat.Remove();

        Assert.Multiple(() =>
        {
            Assert.That(Seat.Extent, Does.Not.Contain(seat));
            Assert.That(_auditorium.Seats, Does.Not.Contain(seat));
        });
    }

    private Seat CreateSeat(int number, int row, Seat.SeatType type)
    {
        return new Seat(number, row, type, _auditorium);
    }
}
