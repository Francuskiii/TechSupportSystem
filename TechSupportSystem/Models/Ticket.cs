using System.ComponentModel.DataAnnotations;

namespace TechSupportSystem.Models;

public class Ticket
{
    [Key]
    public int TicketId { get; set; }

    [Required]
    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string Priority { get; set; } = "Low";

    [Required]
    [MaxLength(20)]
    public string Status { get; set; } = "Open";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // MANY-to-MANY with Technician
    public List<Technician> Technicians { get; set; } = new();

    // ONE-to-MANY with Note
    public List<Note> Notes { get; set; } = new();
}