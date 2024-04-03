using System.Net.Http.Json;
using BioTranan.Core.Dtos;
using BioTranan.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace BioTranan.Tests.IntegrationTests.BioTranan.Api;

public class FilmControllerTests
{
    [Fact]
    public async Task AddFilmAddsFilmToDatabase()
    {
        // Arrange
        var application = new BioTrananWebApplicationFactory();

        var client = application.CreateClient();

        Film film = new() { Title = "Rear Window", ImdbId = "tt112324", RuntimeMinutes = 90, ScreeningsAllowed = 5 };

        // Act
        HttpResponseMessage? response = await client.PostAsJsonAsync("api/1/film", film);

        response.EnsureSuccessStatusCode();

        FilmDto? filmResponse = await response.Content.ReadFromJsonAsync<FilmDto>();

        // Assert
        Assert.Equal("tt112324", filmResponse.ImdbId);
    }
}