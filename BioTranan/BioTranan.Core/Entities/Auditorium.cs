using System.ComponentModel.DataAnnotations;

namespace BioTranan.Core.Entities;

public class Auditorium
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Seats is required")]
    public uint Seats { get; set; }
}