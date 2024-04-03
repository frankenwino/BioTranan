using BioTranan.Core.Entities;

namespace BioTranan.Core.Interfaces;

public interface IAuditoriumService
{
    Task AddAuditoriumAsync(Auditorium auditorium);
    Task<Auditorium> GetAuditoriumByIdAsync(int id);
    Task<Auditorium> GetAuditoriumByNameAsync(string name);
    Task<List<Auditorium>> GetAuditoriumsAsync();
    Task UpdateAuditoriumAsync(int id, Auditorium updatedAuditorium);
    Task DeleteAuditoriumAsync(int id);
}