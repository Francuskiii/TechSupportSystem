namespace TechSupportSystem.DTOs;
public class NoteResponseDto
{
    public int NoteId { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public int TicketId { get; set; }
}