using Microsoft.AspNetCore.Mvc;
using BioTranan.OmdbApi.Models;
using BioTranan.OmdbApi.Dtos;
using BioTranan.Core.Interfaces;

namespace BioTranan.Api.Controllers;

[Route("api/1/omdb")]
[ApiController]
public class OmdbController : ControllerBase
{
    // GET: api/omdb/tt0075859
    [HttpGet("{imdbId}")]
    public async Task<ActionResult<OmdbMovieDto>> GetOmdbMovieByImdbId(IFilmService filmService, string imdbId)
    {
        OmdbMovie omdbMovie;

        imdbId = imdbId.ToLower();

        try
        {
            omdbMovie = await filmService.GetOmdbMovieByImdbIdAsync(imdbId);
        }
        catch (System.Text.Json.JsonException)
        {
            return NotFound();
        }

        return ConvertOmdbMovieToOmbdMovieDto(omdbMovie);
    }

    private OmdbMovieDto ConvertOmdbMovieToOmbdMovieDto(OmdbMovie omdbMovie)
    {
        OmdbMovieDto omdbMovieDto = new OmdbMovieDto(
            omdbMovie.Title,
            omdbMovie.Year,
            omdbMovie.Rated,
            omdbMovie.Released,
            omdbMovie.Runtime,
            omdbMovie.Genre,
            omdbMovie.Director,
            omdbMovie.Writer,
            omdbMovie.Actors,
            omdbMovie.Plot,
            omdbMovie.Language,
            omdbMovie.Country,
            omdbMovie.Awards,
            omdbMovie.Poster,
            omdbMovie.Metascore,
            omdbMovie.ImdbRating,
            omdbMovie.ImdbVotes,
            omdbMovie.ImdbId,
            omdbMovie.Type,
            omdbMovie.DVD,
            omdbMovie.BoxOffice,
            omdbMovie.Production,
            omdbMovie.Website,
            omdbMovie.Response
            );

        return omdbMovieDto;
    }
}