using System.ComponentModel.DataAnnotations;

namespace BioTranan.Core.Dtos;

public record class ScreeningCreateDto(
    [Required(ErrorMessage = "FilmId is required")]
     int FilmId,

    [Required(ErrorMessage = "AuditoriumId is required")]
    int AuditoriumId,

    [Required(ErrorMessage = "Starts is required")]
    DateTime Starts,

    [Required(ErrorMessage = "TicketPrice is required")]
    int TicketPrice,

    [Required(ErrorMessage = "SeatRestriction is required")]
    bool SeatRestriction
);