using System.ComponentModel.DataAnnotations;

namespace BioTranan.Core.Dtos;

public record class AuditoriumUpdateDto(
    [Required(ErrorMessage = "Name is required")]
    [MinLength(1, ErrorMessage = "Name must have at least one character")]
    string Name,

    [Required(ErrorMessage = "Seats is required")]
    uint Seats
);