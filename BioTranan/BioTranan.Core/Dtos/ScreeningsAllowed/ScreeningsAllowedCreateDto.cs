using System.ComponentModel.DataAnnotations;

namespace BioTranan.Core.Dtos;

public record class ScreeningsAllowedCreateDto
{
    [Required(ErrorMessage = "ScreeningsAllowed is required")]
    public uint ScreeningsAllowed { get; init; }
    public ScreeningsAllowedCreateDto(
            uint screeningsAllowed
            )
    {
        ScreeningsAllowed = screeningsAllowed;
    }
}
