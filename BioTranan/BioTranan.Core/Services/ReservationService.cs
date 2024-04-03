using System.Security.Cryptography;
using System.Text;
using BioTranan.Core.Database;
using BioTranan.Core.Entities;
using BioTranan.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BioTranan.Core.Services;

public class ReservationService : IReservationService
{
    private readonly BioTrananDbContext _dbContext;
    private readonly IScreeningService _screeningService;

    public ReservationService(BioTrananDbContext dbContext, IScreeningService screeningService)
    {
        _dbContext = dbContext;
        _screeningService = screeningService;
    }

    public async Task AddReservationAsync(Reservation reservation)
    {
        Screening screening = await _screeningService.GetScreeningByIdAsync(reservation.ScreeningId);

        screening.IncrementBookedSeats(reservation.Seats);

        _dbContext.Screenings.Update(screening);

        await _dbContext.Reservations.AddAsync(reservation);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<Reservation> GetByReservationIdAsync(int id)
    {
        Reservation? reservation = await _dbContext.Reservations
            .Include(r => r.Screening)
            .Include(r => r.Screening.Film)
            .Include(r => r.Screening.Auditorium)
            .FirstOrDefaultAsync(r => r.Id == id);

        return reservation;
    }

    public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
    {
        IEnumerable<Reservation> reservations = await _dbContext.Reservations
            .Include(r => r.Screening)
            .Include(r => r.Screening.Auditorium)
            .Include(r => r.Screening.Film)
            .ToListAsync();

        return reservations;
    }

    public async Task<IEnumerable<Reservation>> GetByScreeningIdAsync(int screeningId)
    {
        IEnumerable<Reservation> reservations = await _dbContext.Reservations
            .Where(r => r.ScreeningId == screeningId)
            .Include(r => r.Screening)
            .Include(r => r.Screening.Auditorium)
            .Include(r => r.Screening.Film)
            .ToListAsync();

        return reservations;
    }

    public async Task DeleteReservationByIdAsync(int id)
    {
        Reservation reservation = await GetByReservationIdAsync(id);

        if (reservation is not null)
        {
            Screening screening = await _screeningService.GetScreeningByIdAsync(reservation.ScreeningId);

            screening.DecrementBookedSeats(reservation.Seats);

            _dbContext.Screenings.Update(screening);

            _dbContext.Reservations.Remove(reservation);

            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<Reservation> GetByReservationCode(string reservationCode)
    {
        Reservation? reservation = await _dbContext.Reservations
            .FirstOrDefaultAsync(r => r.ReservationCode == reservationCode);

        return reservation;
    }

    public async Task UpdateReservation(Reservation reservation)
    {
        _dbContext.Reservations.Update(reservation);

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteReservationsOlderThan(int years = 1)
    {
        IEnumerable<Reservation> oldReservationsToRemove = _dbContext.Reservations
            .Where(r => r.ReservationDate <= DateTime.Now.AddYears(-years));

        _dbContext.Reservations.RemoveRange(oldReservationsToRemove);

        await _dbContext.SaveChangesAsync();
    }

    public string GenerateReservationCode()
    {
        string input = DateTime.Now.ToString();

        byte[] inputBytes = Encoding.UTF8.GetBytes(input);

        // Compute MD5 hash
        using (MD5 md5 = MD5.Create())
        {
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Hash bytes -> hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                // Format byte as hexadecimal string
                sb.Append(hashBytes[i].ToString("x2"));
            }

            // Return first 8 characters of the hexadecimal hash as reservation code
            return sb.ToString().Substring(0, 8).ToUpper();
        }
    }
}