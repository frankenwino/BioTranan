using BioTranan.Core.Entities;

namespace BioTranan.Core.Interfaces;

public interface IReservationService
{
    Task<IEnumerable<Reservation>> GetAllReservationsAsync();
    Task<Reservation> GetByReservationIdAsync(int id);
    Task<IEnumerable<Reservation>> GetByScreeningIdAsync(int screeningId);
    Task AddReservationAsync(Reservation Reservation);
    Task DeleteReservationByIdAsync(int id);
    Task DeleteReservationsOlderThan(int years = 1);
    Task<Reservation> GetByReservationCode(string reservationCode);
    Task UpdateReservation(Reservation reservation);
    string GenerateReservationCode();
}
