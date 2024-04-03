using System.ComponentModel.DataAnnotations;

namespace BioTranan.Core.Entities;

public class Screening
{
    public int Id { get; set; }

    [Required(ErrorMessage = "FilmId is required")]
    public required int FilmId { get; set; }

    public Film? Film { get; set; }

    [Required(ErrorMessage = "AuditoriumId is required")]
    public required int AuditoriumId { get; set; }

    public Auditorium? Auditorium { get; set; }

    [Required(ErrorMessage = "Starts is required")]
    public required DateTime Starts { get; set; }

    [Required(ErrorMessage = "TicketPrice is required")]
    public required int TicketPrice { get; set; }

    public uint BookedSeats { get; set; } = 0;

    public uint TotalSeatsForScreening
    {
        get
        {
            uint totalSeats = Auditorium.Seats;

            if (SeatRestriction)
            {
                totalSeats = totalSeats / 2;
            }
            return totalSeats;
        }
    }

    public uint AvailableSeats
    {
        get
        {
            return TotalSeatsForScreening - BookedSeats;
        }
    }

    [Required(ErrorMessage = "Seats is required")]
    public required bool SeatRestriction { get; set; }

    public void IncrementBookedSeats(uint seats)
    {
        BookedSeats += seats;
    }

    internal void DecrementBookedSeats(uint seats)
    {
        BookedSeats -= seats;
    }
}