using System.Runtime.InteropServices.JavaScript;
using Project.Classes;
using Project.Classes.AudioFormat;
using Project.Classes.PictureFormat;

namespace ProjectTests;

[TestFixture]
public class MovieAssociationTests
{
    [SetUp]
    public void Setup()
    {
        Movie.LoadExtent(new List<Movie>());
        Genre.LoadExtent(new List<Genre>());
    }

    [Test]
    public void Constructor_SetsBiDirectionalAssociation()
    {
        var actionGenre = new Genre("Action");
        var genreList = new List<Genre> { actionGenre };
        var movie = new Movie("John Wick", 101, genreList, DateTime.Today.AddDays(-10), new List<string> { "English" });
        Assert.That(movie.Genres, Contains.Item(actionGenre), "Movie should contain the genre.");
        Assert.That(actionGenre.Movies, Contains.Item(movie), "Genre should contain the reference to the movie.");
    }

    [Test]
    public void Constructor_MultipleGenres_UpdatesAllGenres()
    {
        var sciFi = new Genre("SciFi");
        var adventure = new Genre("Adventure");
        var genres = new List<Genre> { sciFi, adventure };
        var movie = new Movie("Dune", 155, genres, DateTime.Today.AddDays(-10), new List<string> { "English" });
        Assert.That(sciFi.Movies, Contains.Item(movie), "First genre should contain the movie.");
        Assert.That(adventure.Movies, Contains.Item(movie), "Second genre should contain the movie.");
    }

    [Test]
    public void Constructor_ThrowsException_WhenGenreListIsEmpty()
    {
        var emptyGenres = new List<Genre>();
        var ex = Assert.Throws<ArgumentException>(() => 
            new Movie("Bad Movie", 90, emptyGenres, DateTime.Today.AddDays(-10), null)
        );
        Assert.That(ex!.Message, Does.Contain("must have at least one genre"));
    }

    [Test]
    public void AddGenre_EstablishesLinkOnBothSides()
    {
        var horror = new Genre("Horror");
        var comedy = new Genre("Comedy");
        var movie = new Movie("Scary Movie", 90, new List<Genre> { horror }, DateTime.Today.AddDays(-10), null);
        movie.AddGenre(comedy);
        Assert.That(movie.Genres.Contains(comedy), "Movie should have the new genre.");
        Assert.That(comedy.Movies, Contains.Item(movie), "The new Genre should have the movie in its list.");
    }

    [Test]
    public void RemoveGenre_RemovesLinkFromBothSides()
    {
        var drama = new Genre("Drama");
        var romance = new Genre("Romance");
        var movie = new Movie("Titanic", 195, new List<Genre> { drama, romance }, DateTime.Today.AddDays(-10), null);
        movie.RemoveGenre(romance);
        Assert.That(movie.Genres, Does.Not.Contain(romance), "Movie should no longer have the genre.");
        Assert.That(romance.Movies, Does.Not.Contain(movie), "The Genre should no longer reference the movie.");
        Assert.That(movie.Genres, Contains.Item(drama));
        Assert.That(drama.Movies, Contains.Item(movie));
    }

    [Test]
    public void AddGenre_DoesNotAddDuplicates()
    {
        var thriller = new Genre("Thriller");
        var movie = new Movie("Joker", 122, new List<Genre> { thriller }, DateTime.Today.AddDays(-10), null);
        movie.AddGenre(thriller);
        Assert.That(movie.Genres.Count, Is.EqualTo(1), "Movie should not have duplicate genres.");
        Assert.That(thriller.Movies.Count, Is.EqualTo(1), "Genre should not have duplicate movie references.");
    }
    
    [Test]
    public void Constructor_AddsMovieToExtent()
    {
        var genre = new Genre("Docu");
        var movie = new Movie("Planet Earth", 60, new List<Genre> { genre }, DateTime.Today.AddDays(-10), null);
        Assert.That(Movie.Extent, Contains.Item(movie), "Movie should be added to the global extent upon creation.");
    }
}