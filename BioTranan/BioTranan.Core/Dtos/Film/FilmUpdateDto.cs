using System.ComponentModel.DataAnnotations;

namespace BioTranan.Core.Dtos;

public record class FilmUpdateDto(
    [Required(ErrorMessage = "Title is required")]
    string Title,

    [Required(ErrorMessage = "ImdbId is required")]
    string ImdbId
    );