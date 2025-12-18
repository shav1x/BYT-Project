using Project.Classes;

namespace ProjectTests;

[TestFixture]
public class StaffTests
{
    [TestCase("Manager")]
    [TestCase("Developer")]
    [TestCase("QA")]
    [TestCase("Support")]
    public void StaffRole_ValidRole_True(string role)
    {
        var staff = new Person(
            "Andrii", "Meshcheriakov", DateTime.Now.AddYears(-20),
            "ma@gmail.com",
            new Phone("+48", "484848484"),
            role, 1200
        );

        Assert.That(staff.Staff?.Role, Is.EqualTo(role));
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase(null)]
    public void StaffRole_InvalidRole_ThrowsException(string? role)
    {
        Assert.That(
            () => new Person(
                "Andrii", "Meshcheriakov", DateTime.Now.AddYears(-20),
                "ma@gmail.com",
                new Phone("+48", "484848484"),
                role!, 1200
            ),
            Throws.TypeOf<ArgumentException>()
        );
    }

    [TestCase(1)]
    [TestCase(100)]
    [TestCase(999.50)]
    public void StaffSalary_ValidSalary_True(decimal salary)
    {
        var staff = new Person(
            "Andrii", "Meshcheriakov", DateTime.Now.AddYears(-20),
            "test@gmail.com",
            new Phone("+48", "484848484"),
            "Developer", salary
        );

        Assert.That(staff.Staff?.Salary, Is.EqualTo(salary));
    }

    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(-500)]
    public void StaffSalary_InvalidSalary_ThrowsException(decimal salary)
    {
        Assert.That(
            () => new Person(
                "Andrii", "Meshcheriakov", DateTime.Now.AddYears(-20),
                "ma@gmail.com",
                new Phone("+48", "484848484"),
                "Developer", salary
            ),
            Throws.TypeOf<ArgumentException>()
        );
    }
    
    [Test]
    public void StaffPhone_ValidPhone_AssignedCorrectly()
    {
        var phone = new Phone("+48", "999111222");
    
        var staff = new Person(
            "Andrii", "Meshcheriakov",
            DateTime.Now.AddYears(-20),
            "test@gmail.com",
            phone,
            "Developer",
            1200
        );

        Assert.That(staff.Phone, Is.EqualTo(phone));
    }

    [Test]
    public void StaffPhone_NullPhone_AssignedCorrectly()
    {
        var staff = new Person(
            "Andrii", "Meshcheriakov",
            DateTime.Now.AddYears(-20),
            "test@gmail.com",
            null,
            "Developer",
            1200
        );

        Assert.That(staff.Phone, Is.Null);
    }

}