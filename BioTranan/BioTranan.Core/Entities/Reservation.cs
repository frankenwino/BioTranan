using System.ComponentModel.DataAnnotations;

namespace BioTranan.Core.Entities;

public class Reservation
{
    public int Id { get; set; }
    public int ScreeningId { get; set; }
    public Screening Screening { get; set; }

    [Required(ErrorMessage = "Seats is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Seats must be a positive integer")]
    public uint Seats { get; set; }

    public string ReservationCode { get; set; }

    public bool ReservationCodeCheckedIn { get; set; } = false;

    [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$")]
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string EmailAddress { get; set; }

    public DateTime ReservationDate { get; set; } = DateTime.Now;
}
