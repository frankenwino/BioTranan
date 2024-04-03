using System.ComponentModel.DataAnnotations;

namespace BioTranan.Core.Dtos;

public record class ReservationCreateDto(
    [Required(ErrorMessage = "ScreeningId is required")]
    int ScreeningId,

    [Required(ErrorMessage = "Seats is required")]
    uint Seats,

    [Required(ErrorMessage = "EmailAddress is required")]
    string EmailAddress
);