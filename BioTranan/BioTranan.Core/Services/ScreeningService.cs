using Microsoft.EntityFrameworkCore;
using BioTranan.Core.Database;
using BioTranan.Core.Entities;
using BioTranan.Core.Interfaces;

namespace BioTranan.Core.Services;

public class ScreeningService : IScreeningService
{
    private readonly BioTrananDbContext _dbContext;
    private readonly IFilmService _filmService;
    private readonly IAuditoriumService _auditoriumService;

    public ScreeningService(BioTrananDbContext dbContext, IFilmService filmService, IAuditoriumService auditoriumService)
    {
        this._dbContext = dbContext;
        this._filmService = filmService;
        this._auditoriumService = auditoriumService;
    }

    public async Task<Screening> AddScreeningAsync(Screening screening)
    {
        Film film = await _filmService.GetFilmByIdAsync(screening.FilmId);

        film.DecrementScreeningsAllowed();

        _dbContext.Films.Update(film);

        await _dbContext.Screenings.AddAsync(screening);

        await _dbContext.SaveChangesAsync();

        return screening;
    }

    public async Task<List<Screening>> GetAllUpcomingScreeningsAsync()
    {
        List<Screening> screenings = await _dbContext.Screenings
            .Where(s => s.Starts >= DateTime.Now)
            .Include(s => s.Film)
            .Include(s => s.Auditorium)
            .OrderBy(s => s.Starts)
            .ToListAsync();

        return screenings;
    }

    public async Task<Screening?> GetScreeningByIdAsync(int id)
    {
        return await _dbContext.Screenings
            .Include(s => s.Film)
            .Include(s => s.Auditorium)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<bool> AuditoriumAvailable(Screening newScreening, Film film)
    {
        IEnumerable<Screening> existingScreenings = await GetAllUpcomingScreeningsAsync();

        // New screening start and end times
        DateTime newScreeningStarts = newScreening.Starts;
        DateTime newScreeningEnds = newScreening.Starts.AddMinutes((int)film.RuntimeMinutes);

        bool auditoriumAvailable = true;

        foreach (Screening existingScreening in existingScreenings)
        {
            if (newScreening.AuditoriumId == existingScreening.AuditoriumId)
            {
                // Existing screening start and end times
                DateTime existingScreeningStarts = existingScreening.Starts;

                DateTime existingScreeningEnds = existingScreening.Starts.AddMinutes((int)film.RuntimeMinutes);

                // Check if new screening start/end times overlap with an existing
                // screening's start/end DateTime range
                bool startOverlap = newScreeningStarts >= existingScreeningStarts
                                && newScreeningStarts <= existingScreeningEnds;

                bool endOverlap = newScreeningEnds >= existingScreeningStarts
                                && newScreeningEnds <= existingScreeningEnds;

                if (startOverlap || endOverlap)
                {
                    auditoriumAvailable = false;

                    break;
                }
            }
        }

        return auditoriumAvailable;
    }
}