using BioTranan.Core.Entities;

namespace BioTranan.Core.Dtos;

public record class ReservationDto(
    int Id,
    int ScreeningId,
    Screening Screening,
    uint Seats,
    string ReservationCode,
    bool ReservationCodeCheckedIn,
    string EmailAddress,
    DateTime ReservationDate
);