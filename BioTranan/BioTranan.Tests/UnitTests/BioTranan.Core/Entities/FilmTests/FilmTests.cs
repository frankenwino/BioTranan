using BioTranan.Core.Entities;

namespace BioTranan.Tests.UnitTests.BioTranan.Core;

public class FilmTests
{
    [Theory]
    [InlineData("Escape from New York", "tt0082340", 90, 5)]
    [InlineData("The Garbage Pail Kids Movie", "tt0093072", 75, 1)]
    public void DecrementScreeningsAllowedDecrementsByOne(string title, string imdbId, int runtimeMinutes, uint screeningsAllowed)
    {
        // Arrange
        Film film = new Film()
        {
            Title = title,
            ImdbId = imdbId,
            RuntimeMinutes = runtimeMinutes,
            ScreeningsAllowed = screeningsAllowed
        };

        // Act
        film.DecrementScreeningsAllowed();

        // Assert
        Assert.Equal(screeningsAllowed - 1, film.ScreeningsAllowed);
    }

    [Fact]
    public void ScreeningsAllowedIsZeroDecrementScreeningsAllowedReturnsZero()
    {
        // Arrange
        Film film = new Film()
        {
            Title = "Escape from New York",
            ImdbId = "tt0082340",
            RuntimeMinutes = 90,
            ScreeningsAllowed = 0
        };

        uint postScreeningsAllowedDecrement = 0;

        // Act
        film.DecrementScreeningsAllowed();

        // Assert
        Assert.Equal(postScreeningsAllowedDecrement, film.ScreeningsAllowed);
    }

    [Theory]
    [InlineData("Escape from New York", "tt0082340", 90, 5)]
    [InlineData("The Garbage Pail Kids Movie", "tt0093072", 75, 1)]
    public void FilmWithOneScreeningReturnsHasScreeningsRemainingTrue(string title, string imdbId, int runtimeMinutes, uint screeningsAllowed)
    {
        // Arrange
        Film film = new Film()
        {
            Title = title,
            ImdbId = imdbId,
            RuntimeMinutes = runtimeMinutes,
            ScreeningsAllowed = screeningsAllowed
        };

        // Act
        bool filmHasScreeningsRemaining = film.HasScreeningsRemaining;

        // Assert
        Assert.True(filmHasScreeningsRemaining);
    }

    [Fact]
    public void FilmWithZeroScreeningsReturnsHasScreeningsRemainingFalse()
    {
        // Arrange
        Film film = new Film()
        {
            Title = "The Garbage Pail Kids Movie",
            ImdbId = "tt0093072",
            RuntimeMinutes = 75,
            ScreeningsAllowed = 0
        };

        // Act
        bool filmHasScreeningsRemaining = film.HasScreeningsRemaining;

        // Assert
        Assert.False(filmHasScreeningsRemaining);
    }
}