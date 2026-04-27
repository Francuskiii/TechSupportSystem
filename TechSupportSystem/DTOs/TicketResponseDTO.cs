namespace TechSupportSystem.DTOs;

public class TicketResponseDto
{
    public int TicketId { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public List<TechnicianDto> Technicians { get; set; } = new();
    public List<NoteDto> Notes { get; set; } = new();
}