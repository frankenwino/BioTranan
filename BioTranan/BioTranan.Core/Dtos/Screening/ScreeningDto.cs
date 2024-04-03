using BioTranan.Core.Entities;

namespace BioTranan.Core.Dtos;

public record class ScreeningDto(
    int Id,
    int FilmId,
    Film? Film,
    int AuditoriumId,
    Auditorium? Auditorium,
    DateTime Starts,
    int TicketPrice,
    uint BookedSeats,
    uint TotalSeatsForScreening,
    uint AvailableSeats,
    bool SeatRestriction
);