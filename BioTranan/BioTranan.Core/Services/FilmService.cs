using Microsoft.EntityFrameworkCore;
using BioTranan.Core.Database;
using BioTranan.Core.Entities;
using BioTranan.OmdbApi.Models;
using BioTranan.OmdbApi;
using BioTranan.TmdbApi;
using BioTranan.Core.Interfaces;

namespace BioTranan.Core.Services;

public class FilmService : IFilmService
{
    private readonly BioTrananDbContext _dbContext;
    private readonly OmdbApiClient _omdbApiClient;
    private readonly TmdbApiClient _tbdbApiCient;

    public FilmService(BioTrananDbContext dbContext, OmdbApiClient omdbApiClient, TmdbApiClient tbdbApiCient)
    {
        this._dbContext = dbContext;
        this._omdbApiClient = omdbApiClient;
        this._tbdbApiCient = tbdbApiCient;
    }

    public async Task AddFilmAsync(Film film)
    {
        await _dbContext.Films.AddAsync(film);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Film>> GetFilmsAsync()
    {
        return await _dbContext.Films.ToListAsync();
    }

    public async Task<Film> GetFilmByIdAsync(int id)
    {
        return await _dbContext.Films.FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<Film> GetFilmByImdbIdAsync(string imdbId)
    {
        return await _dbContext.Films.FirstOrDefaultAsync(f => f.ImdbId == imdbId);
    }

    public async Task DeleteFilmAsync(int id)
    {
        Film film = await GetFilmByIdAsync(id);

        if (film is not null)
        {
            _dbContext.Films.Remove(film);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteFilmByImdbIdAsync(string imdbId)
    {
        Film film = await GetFilmByImdbIdAsync(imdbId);

        if (film is not null)
        {
            _dbContext.Films.Remove(film);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<OmdbMovie> GetOmdbMovieByImdbIdAsync(string imdbId)
    {
        OmdbMovie omdbMovie = await _omdbApiClient.GetMovieByImdbIdAsync(imdbId);

        return omdbMovie;
    }

    public async Task<string> GetYouTubeTrailerKeyByImdbIdAsync(string imdbId)
    {
        string youTubeTrailerKey = await _tbdbApiCient.GetYouTubeTrailerLinkAsync(imdbId);

        return youTubeTrailerKey;
    }
}