using Microsoft.AspNetCore.Mvc;
using BioTranan.Core.Entities;
using BioTranan.Core.Dtos;
using BioTranan.OmdbApi.Models;
using BioTranan.OmdbApi.Dtos;
using BioTranan.Core.Interfaces;

namespace BioTranan.Api.Controllers;

[Route("api/1/film")]
[ApiController]
public class FilmController : ControllerBase
{
    // GET: api/1/film
    [HttpGet]
    public async Task<IEnumerable<Film>> GetAllFilms(IFilmService filmService)
    {
        IEnumerable<Film> films = await filmService.GetFilmsAsync();

        return films;
    }

    // GET: api/1/film/1
    [HttpGet("{id:int}")]
    public async Task<ActionResult<FilmDto>> GetFilmById(IFilmService filmService, int id)
    {
        Film film = await filmService.GetFilmByIdAsync(id);

        if (film is null)
        {
            return NotFound();
        }

        return ConvertFilmToFilmDto(film);
    }

    // GET: api/1/film/tt0066999
    [HttpGet("{imdbId}")]
    public async Task<ActionResult<FilmDto>> GetFilmByImdbId(IFilmService filmService, string imdbId)
    {

        imdbId = imdbId.ToLower();

        Film film = await filmService.GetFilmByImdbIdAsync(imdbId);

        if (film is null)
        {
            return NotFound();
        }

        return ConvertFilmToFilmDto(film);
    }

    // POST: api/1/film
    [HttpPost]
    public async Task<ActionResult<FilmDto>> AddFilm(FilmCreateDto filmCreateDto, IFilmService filmService)
    {
        Film film = ConvertFilmCreateDtoToFilm(filmCreateDto);

        try
        {
            await filmService.AddFilmAsync(film);
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateException)
        {
            return Conflict($"ImDbId {film.ImdbId} already exists");
        }

        FilmDto filmDto = ConvertFilmToFilmDto(film);

        return CreatedAtAction(nameof(GetFilmById), new { id = filmDto.Id }, filmDto);
    }

    // POST: api/1/film/{imdbId}
    [HttpPost("{imdbId}")]
    public async Task<ActionResult<FilmDto>> AddFilmByImdbIb(ScreeningsAllowedCreateDto screeningsAllowedCreateDto, string imdbId, IFilmService filmService)
    {
        OmdbMovie omdbMovie;

        try
        {
            omdbMovie = await filmService.GetOmdbMovieByImdbIdAsync(imdbId);
        }
        catch (System.Text.Json.JsonException)
        {
            return NotFound($"Movie with ImdbId {imdbId} not found in the Open Movie Database");
        }

        Film film = ConvertOmdbMovieToFilm(omdbMovie, screeningsAllowedCreateDto);

        try
        {
            await filmService.AddFilmAsync(film);
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateException)
        {
            return Conflict($"ImDbId {film.ImdbId} already exists");
        }

        FilmDto filmDto = ConvertFilmToFilmDto(film);

        return CreatedAtAction(nameof(GetFilmById), new { id = filmDto.Id }, filmDto);
    }

    // DELETE api/1/film/{id:int}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteFilm(int id, IFilmService filmService)
    {
        Film film = await filmService.GetFilmByIdAsync(id);

        if (film == null)
        {
            return NotFound();
        }

        await filmService.DeleteFilmAsync(id);

        return NoContent();
    }

    // DELETE api/1/film/{imdbId}
    [HttpDelete("{imdbId}")]
    public async Task<IActionResult> DeleteFilmByImdbId(string imdbId, IFilmService filmService)
    {
        Film film = await filmService.GetFilmByImdbIdAsync(imdbId);

        if (film == null)
        {
            return NotFound();
        }

        await filmService.DeleteFilmByImdbIdAsync(imdbId);

        return NoContent();
    }

    private FilmDto ConvertFilmToFilmDto(Film film)
    {
        FilmDto filmDto = new FilmDto(
            film.Id,
            film.Title,
            film.Year,
            film.Rated,
            film.Released,
            film.RuntimeMinutes,
            film.Genre,
            film.Director,
            film.Writer,
            film.Actors,
            film.Plot,
            film.Language,
            film.Country,
            film.Awards,
            film.Poster,
            film.Metascore,
            film.ImdbRating,
            film.ImdbVotes,
            film.ImdbId,
            film.Type,
            film.DVD,
            film.BoxOffice,
            film.Production,
            film.Website,
            film.ScreeningsAllowed,
            film.HasScreeningsRemaining
            );

        return filmDto;
    }

    private Film ConvertFilmCreateDtoToFilm(FilmCreateDto filmCreateDto)
    {
        Film film = new()
        {
            Title = filmCreateDto.Title,
            ImdbId = filmCreateDto.ImdbId,
            RuntimeMinutes = filmCreateDto.RuntimeMinutes,
            ScreeningsAllowed = (uint)filmCreateDto.ScreeningsAllowed
        };

        return film;
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

    private Film ConvertOmdbMovieToFilm(OmdbMovie omdbMovie, ScreeningsAllowedCreateDto screeningsAllowedCreateDto)
    {
        Film film = new Film()
        {
            Title = omdbMovie.Title,
            Year = omdbMovie.Year,
            Rated = omdbMovie.Rated,
            Released = omdbMovie.Released,
            RuntimeMinutes = Int32.Parse(omdbMovie.Runtime.Split(" ")[0].Trim()),
            Genre = omdbMovie.Genre,
            Director = omdbMovie.Director,
            Writer = omdbMovie.Writer,
            Actors = omdbMovie.Actors,
            Plot = omdbMovie.Plot,
            Language = omdbMovie.Language,
            Country = omdbMovie.Country,
            Awards = omdbMovie.Awards,
            Poster = omdbMovie.Poster,
            Metascore = omdbMovie.Metascore,
            ImdbRating = omdbMovie.ImdbRating,
            ImdbVotes = omdbMovie.ImdbVotes,
            ImdbId = omdbMovie.ImdbId,
            Type = omdbMovie.Type,
            DVD = omdbMovie.DVD,
            BoxOffice = omdbMovie.BoxOffice,
            Production = omdbMovie.Production,
            Website = omdbMovie.Website,
            ScreeningsAllowed = (uint)screeningsAllowedCreateDto.ScreeningsAllowed
        };
        return film;
    }
}