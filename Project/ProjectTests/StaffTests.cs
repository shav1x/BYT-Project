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
        var staff = new Staff(
            "Andrii", "Meshcheriakov", DateTime.Now.AddYears(-20),
            "ma@gmail.com", "4848484848",
            role, 1200
        );

        Assert.That(staff.Role, Is.EqualTo(role));
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase(null)]
    public void StaffRole_InvalidRole_ThrowsException(string? role)
    {
        Assert.That(
            () => new Staff(
                "Andrii", "Meshcheriakov", DateTime.Now.AddYears(-20),
                "ma@gmail.com", "4848484848",
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
        var staff = new Staff(
            "Andrii", "Meshcheriakov", DateTime.Now.AddYears(-20),
            "test@gmail.com", "4848484848",
            "Developer", salary
        );

        Assert.That(staff.Salary, Is.EqualTo(salary));
    }

    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(-500)]
    public void StaffSalary_InvalidSalary_ThrowsException(decimal salary)
    {
        Assert.That(
            () => new Staff(
                "Andrii", "Meshcheriakov", DateTime.Now.AddYears(-20),
                "ma@gmail.com", "4848484848",
                "Developer", salary
            ),
            Throws.TypeOf<ArgumentException>()
        );
    }
}
