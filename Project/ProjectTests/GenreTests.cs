using Project.Classes;

namespace ProjectTests;

[TestFixture]
public class GenreTests
{
    [SetUp]
    public void SetUp()
    {
        Genre.LoadExtent(new List<Genre>());
    }

    [Test]
    public void Constructor_ValidMovieGenre_CreatesGenre()
    {
        var genre = new Genre("Action");

        Assert.That(genre.Name, Is.EqualTo("Action"));
        Assert.That(Genre.Extent.Count, Is.EqualTo(1));
        Assert.That(Genre.Extent[0], Is.EqualTo(genre));
    }

    [TestCase("Action")]
    [TestCase("Comedy")]
    [TestCase("Drama")]
    public void GenreName_ValidMovieGenre_SetsCorrectly(string movieGenre)
    {
        var genre = new Genre();
        genre.Name = movieGenre;

        Assert.That(genre.Name, Is.EqualTo(movieGenre));
    }

    [TestCase("")]
    [TestCase("   ")]
    [TestCase("Sci-Fi")]
    [TestCase("Action1")]
    [TestCase("Romance!")]
    [TestCase("123")]
    public void GenreName_InvalidMovieGenre_ThrowsArgumentException(string invalidMovieGenre)
    {
        var genre = new Genre();

        Assert.That(
            () => genre.Name = invalidMovieGenre,
            Throws.TypeOf<ArgumentException>()
        );
    }

    [Test]
    public void Constructor_InvalidMovieGenre_ThrowsArgumentException()
    {
        Assert.That(
            () => new Genre("Horror123"),
            Throws.TypeOf<ArgumentException>()
        );
        
        Assert.That(Genre.Extent, Is.Empty);
    }
}
