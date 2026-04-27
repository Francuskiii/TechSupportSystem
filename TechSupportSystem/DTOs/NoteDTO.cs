namespace TechSupportSystem.DTOs;

public class NoteDto
{
    public int NoteId { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}