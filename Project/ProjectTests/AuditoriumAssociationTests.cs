using Project.Classes;
using Project.Classes.AudioFormat;
using Project.Classes.PictureFormat;

namespace ProjectTests;

[TestFixture]
public class AuditoriumAssociationTests
{
    private Auditorium _auditorium;
    private ScreeningProfile _screeningProfile;

    [SetUp]
    public void SetUp()
    {
        _screeningProfile = new ScreeningProfile(
            ScreeningProfile.ResolutionType._4K,
            120,
            new Stereo("Dolby Digital"),
            new FormatImax(true));

        _auditorium = new Auditorium(1, "Premium Auditorium", _screeningProfile);
    }

    [Test]
    public void AddSeat_ValidSeat_AddedToAuditorium()
    {
        var seat = new Seat(101, 1, Seat.SeatType.VIP, _auditorium);

        Assert.Multiple(() =>
        {
            Assert.That(_auditorium.Seats, Contains.Item(seat));
            Assert.That(seat.Auditorium, Is.EqualTo(_auditorium));
        });
            
        seat.Remove();
    }

    [Test]
    public void RemoveSeat_ValidSeat_RemovedFromAuditorium()
    {
        var seat = new Seat(101, 1, Seat.SeatType.VIP, _auditorium);
            
        _auditorium.RemoveSeat(seat);

        Assert.Multiple(() =>
        {
            Assert.That(_auditorium.Seats, Does.Not.Contain(seat));
            Assert.That(_auditorium.Seats.Count, Is.EqualTo(0));
        });
    }

    [Test]
    public void SetScreeningProfile_ValidProfile_ChangesAuditoriumProfile()
    {
        var newProfile = new ScreeningProfile(
            ScreeningProfile.ResolutionType.FullHD,
            60,
            new Stereo("Legacy Codec"),
            new ThreeD(false));
            
        _auditorium.SetScreeningProfile(newProfile);

        Assert.That(_auditorium.ScreeningProfile, Is.EqualTo(newProfile));
        Assert.That(newProfile.Auditorium, Is.EqualTo(_auditorium));
    }

    [Test]
    public void SetScreeningProfile_ProfileAlreadyInUse_ThrowsInvalidOperationException()
    {
        _screeningProfile.SetAuditorium(null);
        var anotherAuditorium = new Auditorium(2, "Secondary Hall", _screeningProfile);

        Assert.Throws<InvalidOperationException>(() => 
            _auditorium.SetScreeningProfile(_screeningProfile));
    }

    [Test]
    public void RemoveAuditorium_SeatsAndProfileCleared()
    {
        var seat = new Seat(101, 1, Seat.SeatType.VIP, _auditorium);
        _auditorium.Remove();

        Assert.Multiple(() =>
        {
            Assert.That(_auditorium.Seats, Is.Empty);
            Assert.That(_screeningProfile.Auditorium, Is.Null);
        });
    }
    
    [Test]
    public void Seat_WithoutAuditorium_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            new Seat(100, 1, Seat.SeatType.VIP, null!));
    }
    
    [Test]
    public void RemoveSeat_ClearsBidirectionalAssociation()
    {
        var seat = new Seat(10, 1, Seat.SeatType.Standard, _auditorium);

        seat.Remove();

        Assert.Multiple(() =>
        {
            Assert.That(_auditorium.Seats, Does.Not.Contain(seat));
            Assert.That(Seat.Extent, Does.Not.Contain(seat));
            Assert.Throws<InvalidOperationException>(() => { var _ = seat.Auditorium; });
        });
    }
    
    [Test]
    public void AddSeat_SameSeatTwice_ShouldNotDuplicate()
    {
        var seat = new Seat(1, 1, Seat.SeatType.VIP, _auditorium);

        _auditorium.AddSeat(seat);
        _auditorium.AddSeat(seat);

        Assert.That(_auditorium.Seats.Count, Is.EqualTo(1));
    }
    
    [Test]
    public void ScreeningProfile_CannotBeAssignedToTwoAuditoriums()
    {
        var secondHall = new Auditorium(2, "Blue Hall", new ScreeningProfile(
            ScreeningProfile.ResolutionType.FullHD,
            30,
            new Stereo("Legacy Codec"),
            new ThreeD(false)));

        Assert.Throws<InvalidOperationException>(() =>
            secondHall.SetScreeningProfile(_screeningProfile));
    }
    
    [Test]
    public void RemoveAuditorium_DestroysSeatsAndClearsProfile()
    {
        var seat1 = new Seat(1, 1, Seat.SeatType.Standard, _auditorium);
        var seat2 = new Seat(2, 1, Seat.SeatType.VIP, _auditorium);

        _auditorium.Remove();

        Assert.Multiple(() =>
        {
            Assert.That(_auditorium.Seats, Is.Empty);
            Assert.That(_screeningProfile.Auditorium, Is.Null);
            Assert.That(Auditorium.Extent, Does.Not.Contain(_auditorium));
        });
    }
}