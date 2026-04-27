using System.ComponentModel.DataAnnotations;

namespace TechSupportSystem.Models;

public class Note
{
    [Key]
    public int NoteId { get; set; }

    [Required]
    [MaxLength(1000)]
    public string Content { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    //FK
    public int TicketId { get; set; }

    //Navigation property
    public Ticket? Ticket { get; set; }
}