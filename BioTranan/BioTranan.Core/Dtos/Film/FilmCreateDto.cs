using System.ComponentModel.DataAnnotations;

namespace BioTranan.Core.Dtos;

public record class FilmCreateDto
{
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; init; }

    [Required(ErrorMessage = "ImdbId is required")]
    public string ImdbId { get; init; }

    [Required(ErrorMessage = "RuntimeMinutes is required")]
    public required int RuntimeMinutes { get; init; }

    [Required(ErrorMessage = "ScreeningsAllowed is required")]
    public required uint ScreeningsAllowed { get; init; }

    public FilmCreateDto(string title, string imdbId, int runtimeMinutes, uint screeningsAllowed)
    {
        Title = title;
        ImdbId = imdbId;
        RuntimeMinutes = runtimeMinutes;
        ScreeningsAllowed = screeningsAllowed;
    }
}