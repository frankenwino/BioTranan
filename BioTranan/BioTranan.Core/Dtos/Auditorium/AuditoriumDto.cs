using System.ComponentModel.DataAnnotations;

namespace BioTranan.Core.Dtos;

public record class AuditoriumDto(
    int Id,

    [Required(ErrorMessage = "Name is required")]
    string Name,

    [Required(ErrorMessage = "Seats is required")]
    uint Seats
);