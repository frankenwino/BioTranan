using BioTranan.Core.Database;
using BioTranan.Core.Entities;
using BioTranan.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BioTranan.Core.Services;

public class AuditoriumService : IAuditoriumService
{
    private readonly BioTrananDbContext _dbContext;

    public AuditoriumService(BioTrananDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public async Task AddAuditoriumAsync(Auditorium auditorium)
    {
        await _dbContext.Auditoriums.AddAsync(auditorium);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Auditorium> GetAuditoriumByIdAsync(int id)
    {
        return await _dbContext.Auditoriums.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Auditorium> GetAuditoriumByNameAsync(string name)
    {
        return await _dbContext.Auditoriums.FirstOrDefaultAsync(a => a.Name == name);
    }

    public async Task<List<Auditorium>> GetAuditoriumsAsync()
    {
        return await _dbContext.Auditoriums.ToListAsync();
    }

    public async Task UpdateAuditoriumAsync(int id, Auditorium updatedAuditorium)
    {
        Auditorium currentAuditorium = await GetAuditoriumByIdAsync(id);

        if (currentAuditorium is not null)
        {
            _dbContext.Entry(currentAuditorium)
                .CurrentValues
                .SetValues(updatedAuditorium);

            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteAuditoriumAsync(int id)
    {
        Auditorium auditorium = await GetAuditoriumByIdAsync(id);

        if (auditorium is not null)
        {
            _dbContext.Auditoriums.Remove(auditorium);
            await _dbContext.SaveChangesAsync();
        }
    }
}