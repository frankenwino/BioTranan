using BioTranan.Core.Entities;

namespace BioTranan.Core.Interfaces;

public interface IScreeningService
{
    Task<Screening> AddScreeningAsync(Screening screening);
    Task<List<Screening>> GetAllUpcomingScreeningsAsync();
    Task<Screening> GetScreeningByIdAsync(int id);
    Task<bool> AuditoriumAvailable(Screening screening, Film film);
}