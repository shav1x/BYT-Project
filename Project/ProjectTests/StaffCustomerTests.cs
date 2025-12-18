using Project.Classes;

namespace ProjectTests;

[TestFixture]
public class StaffCustomerTests
{
    [Test]
    public void Constructor_ValidData_CreatesStaffAndCustomer()
    {
        var phone = new Phone("+48", "123456789");
        var birthDate = DateTime.Today.AddYears(-30);
        
        var person = new Person("John", "Doe", birthDate, "john.doe@example.com", phone, "Manager", 50000, true);
        
        Assert.Multiple(() =>
        {
            Assert.That(person.Name, Is.EqualTo("John"));
            Assert.That(person.Surname, Is.EqualTo("Doe"));
            Assert.That(person.Age, Is.EqualTo(30));
            Assert.That(person.Email, Is.EqualTo("john.doe@example.com"));
            Assert.That(person.Phone, Is.SameAs(phone));
            
            Assert.That(person.Staff, Is.Not.Null);
            Assert.That(person.Staff?.Role, Is.EqualTo("Manager"));
            Assert.That(person.Staff?.Salary, Is.EqualTo(50000));
            Assert.That(person.Staff?.Person, Is.SameAs(person));
            
            Assert.That(person.Customer, Is.Not.Null);
            Assert.That(person.Customer?.BonusPoints, Is.EqualTo(0));
            Assert.That(person.Customer?.AccountId, Is.GreaterThan(0));
            Assert.That(person.Customer?.Person, Is.SameAs(person));
        });
    }

    [TestCase(0)]
    [TestCase(-1000)]
    public void Constructor_InvalidSalary_ThrowsArgumentException(decimal salary)
    {
        Assert.That(() => new Person("Jane", "Smith", DateTime.Today.AddYears(-25), "jane.smith@example.com", null, "Developer", salary, true),
            Throws.ArgumentException.With.Message.EqualTo("Salary must be positive"));
    }
    
    [TestCase("")]
    [TestCase(" ")]
    public void Constructor_InvalidRole_ThrowsArgumentException(string role)
    {
        Assert.That(() => new Person("Jane", "Smith", DateTime.Today.AddYears(-25), "jane.smith@example.com", null, role, 60000, true),
            Throws.ArgumentException.With.Message.EqualTo("Role cannot be empty"));
    }
    
    [Test]
    public void Constructor_AlsoCustomerIsFalse_CreatesOnlyStaff()
    {
        var person = new Person("Peter", "Jones", DateTime.Today.AddYears(-40), "peter.jones@example.com", null, "Analyst", 70000, false);
        
        Assert.Multiple(() =>
        {
            Assert.That(person.Staff, Is.Not.Null);
            Assert.That(person.Customer, Is.Null);
        });
    }
}
