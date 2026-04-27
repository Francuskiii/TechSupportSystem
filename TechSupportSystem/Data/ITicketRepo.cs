using TechSupportSystem.DTOs;
using TechSupportSystem.Models;

namespace TechSupportSystem.Data;

public interface ITicketRepo
{
    Task<List<Ticket>> GetAllTicketsAsync();
    Task<Ticket?> GetTicketByIdAsync(int id);
    Task<Ticket> CreateTicketAsync(Ticket newTicket);

    Task UpdateTicketStatusAsync(int id, UpdateTicketStatusDTO updateStatus);

    Task AssignTechnicianAsync(AssignTicketDTO tick);
    Task<Note> CreateNoteAsync(int id, CreateNoteDTO note);
    Task DeleteTicketAsync(Ticket ticketToDelete);


}