using BioTranan.Core.Entities;
using BioTranan.OmdbApi.Models;

namespace BioTranan.Core.Interfaces;

public interface IFilmService
{
    Task<List<Film>> GetFilmsAsync();
    Task<Film> GetFilmByIdAsync(int id);
    Task<Film> GetFilmByImdbIdAsync(string imdbId);
    Task AddFilmAsync(Film film);
    Task DeleteFilmAsync(int id);
    Task DeleteFilmByImdbIdAsync(string imdbId);
    Task<OmdbMovie> GetOmdbMovieByImdbIdAsync(string imdbId);
    Task<string> GetYouTubeTrailerKeyByImdbIdAsync(string imdbId);
}