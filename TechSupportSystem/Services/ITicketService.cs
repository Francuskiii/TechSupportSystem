using TechSupportSystem.Models;
using TechSupportSystem.DTOs;

namespace TechSupportSystem.Services;

public interface ITicketService
{
    Task<List<TicketResponseDto>> GetAllTicketsAsync();
    Task<TicketResponseDto?> GetTicketByIdAsync(int id);

    Task <TicketResponseDto> CreateTicketAsync(NewTicketDTO newTicket);

    Task UpdateTicketStatusAsync(int id, UpdateTicketStatusDTO updateStatus);

    Task AssignTechnicianAsync(AssignTicketDTO tick);

    Task DeleteTicketAsync(int ticketId);

    Task<NoteResponseDto?>CreateNoteAsync(int id, CreateNoteDTO note);


}