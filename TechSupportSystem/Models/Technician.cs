using System.ComponentModel.DataAnnotations;

namespace TechSupportSystem.Models;

public class Technician
{
    [Key]
    public int TechnicianId { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Range(1, 3)]
    public int Level { get; set; }

    // MANY-to-MANY with Ticket
    public List<Ticket> Tickets { get; set; } = new();
}