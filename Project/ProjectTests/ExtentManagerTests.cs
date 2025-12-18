using Project.Extent;
using Project.Classes;

namespace ProjectTests;

[TestFixture]
public class ExtentManagerTests
{
    private string _testDirectory = null!;
    private string _testFile = null!;

    [SetUp]
    public void Setup()
    {
        _testDirectory = Path.Combine(Path.GetTempPath(), "ExtentTest_" + Guid.NewGuid());
        Directory.CreateDirectory(_testDirectory);
        
        _testFile = Path.Combine(_testDirectory, "extent.json");
        
        ExtentManager.SetFileNameForTesting(_testFile);
        
        Customer.LoadExtent(new());
        Staff.LoadExtent(new());
        Movie.LoadExtent(new());
        Genre.LoadExtent(new());
    }
    
    [TearDown]
    public void TearDown()
    {
        if (Directory.Exists(_testDirectory))
            Directory.Delete(_testDirectory, true);
    }

    [Test]
    public void SaveAll_CreatesFile()
    {
        ExtentManager.SaveAll();
        Assert.That(File.Exists(_testFile), Is.True);
    }
    
    [Test]
    public void SaveAll_EmptyExtents_ValidJson()
    {
        ExtentManager.SaveAll();

        string json = File.ReadAllText(_testFile);
        Assert.That(json, Does.Contain("Customers"));
        Assert.That(json, Does.Contain("StaffMembers"));
        Assert.That(json, Does.Contain("Movies"));
        Assert.That(json, Does.Contain("Genres"));
    }

    [Test]
    public void LoadAll_NoFile_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => ExtentManager.LoadAll());
        Assert.That(Customer.Extent.Count, Is.EqualTo(0));
        Assert.That(Staff.Extent.Count, Is.EqualTo(0));
    }

    [Test]
    public void SaveAndLoad_RestoresCustomers()
    {
        var c1 = new Person(
            "Andrii", "Meshcheriakov",
            DateTime.Now.AddYears(-20),
            "mail@mail.com",
            new Phone("+1", "12345")
        );

        var c2 = new Person(
            "Bohdan", "Milashevskyi",
            DateTime.Now.AddYears(-30),
            "gg@mail.com",
            new Phone("+1", "54321")
        );

        ExtentManager.SaveAll();

        Customer.LoadExtent(new());

        Assert.That(Customer.Extent.Count, Is.EqualTo(0));

        ExtentManager.LoadAll();

        Assert.That(Customer.Extent.Count, Is.EqualTo(2));
        Assert.That(Customer.Extent.First().Person.Name, Is.EqualTo("Andrii"));
        Assert.That(Customer.Extent.Last().Person.Email, Is.EqualTo("gg@mail.com"));
    }

    [Test]
    public void SaveAndLoad_RestoresMoviesAndGenres()
    {
        var action = new Genre("Action");
        var comedy = new Genre("Comedy");

        var m1 = new Movie("Inception", 148, new List<Genre> { action }, new DateTime(2010, 1, 1), new List<string> { "English" }, 13);
        var m2 = new Movie("Matrix", 136, new List<Genre> { action, comedy }, new DateTime(1999, 1, 1), new List<string> { "English" }, 16);

        ExtentManager.SaveAll();

        Movie.LoadExtent(new());
        Genre.LoadExtent(new());

        ExtentManager.LoadAll();

        Assert.That(Movie.Extent.Count, Is.EqualTo(2));
        Assert.That(Genre.Extent.Count, Is.EqualTo(2));
        Console.WriteLine(Movie.Extent.Where(name => name.Name == "Inception"));
        Assert.That(Movie.Extent.First().Name, Is.EqualTo("Inception"));
        Assert.That(Movie.Extent.Last().Genres.Count, Is.EqualTo(2));   
    }

    [Test]
    public void LoadAll_EmptyJson_DoesNotThrow()
    {
        File.WriteAllText(_testFile, "");

        Assert.DoesNotThrow(() => ExtentManager.LoadAll());
    }
}
