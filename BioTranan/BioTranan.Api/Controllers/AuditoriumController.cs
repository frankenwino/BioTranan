using Microsoft.AspNetCore.Mvc;
using BioTranan.Core.Entities;
using BioTranan.Core.Dtos;
using BioTranan.Core.Interfaces;

namespace BioTranan.Api.Controllers;

[Route("api/1/auditorium")]
[ApiController]
public class AuditoriumController : ControllerBase
{
    // GET: api/1/auditorium
    [HttpGet]
    public async Task<IEnumerable<Auditorium>> GetAllAuditoriums(IAuditoriumService auditoriumService)
    {
        IEnumerable<Auditorium> auditoriums = await auditoriumService.GetAuditoriumsAsync();

        return auditoriums;
    }

    // GET: api/auditorium/1
    [HttpGet("{id:int}")]
    public async Task<ActionResult<AuditoriumDto>> GetAuditoriumById(IAuditoriumService auditoriumService, int id)
    {
        Auditorium auditorium = await auditoriumService.GetAuditoriumByIdAsync(id);

        if (auditorium is null)
        {
            return NotFound();
        }

        return ConvertAuditoriumToAuditoriumDto(auditorium);
    }

    // POST: api/auditorium
    [HttpPost]
    public async Task<ActionResult<AuditoriumDto>> AddAuditorium(AuditoriumCreateDto auditoriumCreateDto, IAuditoriumService auditoriumService)
    {
        Auditorium auditorium = ConvertAuditoriumCreateDtoToAuditorium(auditoriumCreateDto);

        try
        {
            await auditoriumService.AddAuditoriumAsync(auditorium);
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateException)
        {
            return Conflict($"Auditorium named '{auditorium.Name}' already exists");
        }

        AuditoriumDto auditoriumDto = ConvertAuditoriumToAuditoriumDto(auditorium);

        return CreatedAtAction(nameof(GetAuditoriumById), new { id = auditoriumDto.Id }, auditoriumDto);
    }

    // PUT: api/auditorium/1
    [HttpPut("{id:int}")]
    public async Task<ActionResult<AuditoriumDto>> UpdateAuditorium(IAuditoriumService auditoriumService, int id, AuditoriumUpdateDto auditoriumUpdateDto)
    {
        Auditorium auditorium = await auditoriumService.GetAuditoriumByIdAsync(id);

        if (auditorium is null)
        {
            return NotFound();
        }

        Auditorium updatedAuditorium = new Auditorium()
        {
            Id = id,
            Name = auditoriumUpdateDto.Name,
            Seats = auditoriumUpdateDto.Seats
        };

        await auditoriumService.UpdateAuditoriumAsync(id, updatedAuditorium);

        return NoContent();
    }

    // DELETE api/auditorium/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAuditorium(IAuditoriumService auditoriumService, int id)
    {
        Auditorium auditorium = await auditoriumService.GetAuditoriumByIdAsync(id);
        if (auditorium == null)
        {
            return NotFound();
        }

        await auditoriumService.DeleteAuditoriumAsync(id);

        return NoContent();
    }

    private AuditoriumDto ConvertAuditoriumToAuditoriumDto(Auditorium auditorium)
    {
        AuditoriumDto auditoriumDto = new AuditoriumDto(
            auditorium.Id,
            auditorium.Name,
            auditorium.Seats
            );

        return auditoriumDto;
    }

    private Auditorium ConvertAuditoriumCreateDtoToAuditorium(AuditoriumCreateDto auditoriumCreateDto)
    {
        Auditorium auditorium = new()
        {
            Name = auditoriumCreateDto.Name,
            Seats = auditoriumCreateDto.Seats
        };

        return auditorium;
    }

    private Auditorium ConvertAuditoriumUpdateDtoToAuditorium(AuditoriumUpdateDto auditoriumUpdateDto)
    {
        Auditorium auditorium = new()
        {
            Name = auditoriumUpdateDto.Name,
            Seats = auditoriumUpdateDto.Seats
        };

        return auditorium;
    }
}