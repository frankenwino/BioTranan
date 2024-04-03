using Microsoft.AspNetCore.Mvc;
using BioTranan.Core.Entities;
using BioTranan.Core.Dtos;
using BioTranan.Core.Interfaces;

namespace BioTranan.Api.Controllers;

[Route("api/1/reservation")]
[ApiController]
public class ReservationController : ControllerBase
{
    // GET: api/1/reservation
    [HttpGet]
    public async Task<IEnumerable<Reservation>> GetAllReservations(IReservationService reservationService)
    {
        IEnumerable<Reservation> reservations = await reservationService.GetAllReservationsAsync();

        return reservations;
    }

    // GET: api/1/reservation/screening/{id:int}
    [HttpGet("screening/{id:int}")]
    public async Task<ActionResult<IEnumerable<ReservationDto>>> GetReservationsByScreeningId(IReservationService reservationService, int id)
    {
        IEnumerable<Reservation> reservations = await reservationService.GetByScreeningIdAsync(id);

        List<ReservationDto> reservationDtos = new List<ReservationDto>();

        foreach (Reservation reservation in reservations)
        {
            reservationDtos.Add(ConvertReservationToReservationDto(reservation));
        }

        return reservationDtos;
    }

    // GET: api/1/reservation/{id:int}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ReservationDto>> GetByReservationId(IReservationService reservationService, int id)
    {
        Reservation reservation = await reservationService.GetByReservationIdAsync(id);

        if (reservation is null)
        {
            return NotFound();
        }

        ReservationDto reservationDto = ConvertReservationToReservationDto(reservation);

        return reservationDto;
    }

    // POST: api/1/reservation
    [HttpPost]
    public async Task<ActionResult<ReservationDto>> AddReservation(ReservationCreateDto reservationCreateDto, IReservationService reservationService, IScreeningService screeningService)
    {
        Reservation reservation = ConvertReservationCreateDtoToReservation(reservationCreateDto);

        Screening screening = await screeningService.GetScreeningByIdAsync(reservation.ScreeningId);

        if (screening.AvailableSeats == 0)
        {
            return BadRequest("Screening sold out");
        }

        if (screening.AvailableSeats < reservation.Seats)
        {
            return BadRequest($"Only {screening.AvailableSeats} seats remaining.");
        }

        reservation.ReservationCode = reservationService.GenerateReservationCode();

        try
        {
            await reservationService.AddReservationAsync(reservation);
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateException)
        {
            return Conflict($"Reservation code '{reservation.ReservationCode}' already exists");
        }

        ReservationDto ReservationDto = ConvertReservationToReservationDto(reservation);

        return CreatedAtAction(nameof(GetByReservationId), new { id = ReservationDto.Id }, ReservationDto);
    }

    // POST: api/1/reservation/checkinreservationcode/{reservationCode}
    [HttpPost("checkinreservationcode/{reservationCode}")]
    public async Task<IActionResult> CheckInReservationCode(string reservationCode, IReservationService reservationService)
    {
        Reservation reservation = await reservationService.GetByReservationCode(reservationCode);

        if (reservation is null)
        {
            return NotFound($"Reservation code {reservationCode} does not exist");
        }

        if (reservation.ReservationCodeCheckedIn == true)
        {
            return BadRequest($"Reservation code {reservation.ReservationCode} already checked in");
        }

        reservation.ReservationCodeCheckedIn = true;

        await reservationService.UpdateReservation(reservation);

        return NoContent();
    }

    // DELETE api/1/reservation/{id:int}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteReservation(IReservationService reservationService, int id)
    {
        Reservation reservation = await reservationService.GetByReservationIdAsync(id);

        if (reservation == null)
        {
            return NotFound();
        }

        await reservationService.DeleteReservationByIdAsync(id);

        return NoContent();
    }

    // DELETE api/1/reservation/olderthan/{years:int}
    [HttpDelete("olderthan/{years:int}")]
    public async Task<IActionResult> DeleteOldReservations(IReservationService reservationService, int years)
    {
        await reservationService.DeleteReservationsOlderThan(years);

        return NoContent();
    }

    private ReservationDto ConvertReservationToReservationDto(Reservation reservation)
    {
        ReservationDto reservationDto = new ReservationDto(
            reservation.Id,
            reservation.ScreeningId,
            reservation.Screening,
            reservation.Seats,
            reservation.ReservationCode,
            reservation.ReservationCodeCheckedIn,
            reservation.EmailAddress,
            reservation.ReservationDate
            );

        return reservationDto;
    }

    private Reservation ConvertReservationCreateDtoToReservation(ReservationCreateDto reservationCreateDto)
    {
        Reservation reservation = new()
        {
            ScreeningId = reservationCreateDto.ScreeningId,
            Seats = reservationCreateDto.Seats,
            EmailAddress = reservationCreateDto.EmailAddress
        };

        return reservation;
    }
}