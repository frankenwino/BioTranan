using System.ComponentModel.DataAnnotations;

namespace BioTranan.Core.Dtos;

public record class FilmDto(
    int Id,
    [Required(ErrorMessage = "Title is required")]
    string Title,
    string Year,
    string Rated,
    string Released,
    [Required(ErrorMessage = "RuntimeMinutes is required")]
    int? RuntimeMinutes,
    string Genre,
    string Director,
    string Writer,
    string Actors,
    string Plot,
    string Language,
    string Country,
    string Awards,
    string Poster,
    string Metascore,
    string ImdbRating,
    string ImdbVotes,
    [Required(ErrorMessage = "ImdbId is required")]
    string ImdbId,
    string Type,
    string DVD,
    string BoxOffice,
    string Production,
    string Website,
    [Required(ErrorMessage = "ScreeningsAllowed is required")]
    uint ScreeningsAllowed,
    bool HasScreeningsRemaining
);