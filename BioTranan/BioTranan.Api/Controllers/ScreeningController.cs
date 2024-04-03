using Microsoft.AspNetCore.Mvc;
using BioTranan.Core.Entities;
using BioTranan.Core.Dtos;
using BioTranan.Core.Interfaces;

namespace BioTranan.Api.Controllers;

[ApiController]
[Route("api/1/screening")]
public class ScreeningController : ControllerBase
{
    // GET: api/1/screening
    [HttpGet]
    public async Task<IEnumerable<Screening>> GetAllUpcomingScreenings(IScreeningService screeningService)
    {
        IEnumerable<Screening> screenings = await screeningService.GetAllUpcomingScreeningsAsync();

        return screenings;
    }

    // GET: api/1/screening/1
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ScreeningDto>> GetScreeningById(IScreeningService screeningService, int id)
    {
        Screening screening = await screeningService.GetScreeningByIdAsync(id);

        if (screening is null)
        {
            return NotFound();
        }

        return ConvertScreeningToScreeningDto(screening);
    }

    // POST: api/1/screening
    [HttpPost]
    public async Task<ActionResult<FilmDto>> AddScreening(ScreeningCreateDto screeningCreateDto, IScreeningService screeningService, IFilmService filmService, IAuditoriumService auditoriumService)
    {
        // Check if film exists in Db
        Film? film = await filmService.GetFilmByIdAsync(screeningCreateDto.FilmId);

        if (film is null)
        {
            return NotFound($"FilmId {screeningCreateDto.FilmId} not found in database");
        }

        // Check if auditorium exists in Db
        Auditorium? auditorium = await auditoriumService.GetAuditoriumByIdAsync(screeningCreateDto.AuditoriumId);
        if (auditorium is null)
        {
            return NotFound($"AuditoriumId {screeningCreateDto.AuditoriumId} not found in database");
        }

        Screening screening = ConvertScreeningCreateDtoToScreening(screeningCreateDto);

        // Check if screening.Starts in the past
        if (screening.Starts < DateTime.Now)
        {
            return BadRequest($"Screening start time is in the past ({screening.Starts.ToString()})");
        }

        // Check if auditorium available
        bool auditoriumAvailable = await screeningService.AuditoriumAvailable(screening, film);

        if (auditoriumAvailable == false)
        {
            return BadRequest("Auditorium is not available");
        }

        // Check if film has screenings available
        if (film.HasScreeningsRemaining == false)
        {
            return BadRequest($"FilmId {film.Id} ({film.Title}) has reached maximum screenings allowed");
        }

        // Business rules passed. Add screening to DB
        await screeningService.AddScreeningAsync(screening);

        ScreeningDto screeningDto = ConvertScreeningToScreeningDto(screening);

        return CreatedAtAction(nameof(GetScreeningById), new { id = screeningDto.Id }, screeningDto);
    }


    private Screening ConvertScreeningCreateDtoToScreening(ScreeningCreateDto screeningCreateDto)
    {
        Screening screening = new()
        {
            FilmId = screeningCreateDto.FilmId,
            AuditoriumId = screeningCreateDto.AuditoriumId,
            Starts = screeningCreateDto.Starts,
            SeatRestriction = screeningCreateDto.SeatRestriction,
            TicketPrice = screeningCreateDto.TicketPrice
        };
        return screening;
    }

    private ScreeningDto ConvertScreeningToScreeningDto(Screening screening)
    {
        ScreeningDto screeningDto = new ScreeningDto(
            screening.Id,
            screening.FilmId,
            screening.Film,
            screening.AuditoriumId,
            screening.Auditorium,
            screening.Starts,
            screening.TicketPrice,
            screening.BookedSeats,
            screening.TotalSeatsForScreening,
            screening.AvailableSeats,
            screening.SeatRestriction
        );

        return screeningDto;
    }
}