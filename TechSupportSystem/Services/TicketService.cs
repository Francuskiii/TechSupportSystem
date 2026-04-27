using System.Security.Principal;
using TechSupportSystem.Data;
using TechSupportSystem.DTOs;
using TechSupportSystem.Mapper;
using TechSupportSystem.Models;

namespace TechSupportSystem.Services;

public class TicketService : ITicketService
{
    private readonly ITicketRepo _repo;

    public TicketService(ITicketRepo repo)
    {
        _repo = repo;
    }

    /* Instead of returning full DB entities, take each Ticket entity and reshape it
    to a DBO object. Otherwise JSON will go into an infinite loop.

    Each ticket has a collection of technicains (Many 2 Many) - Loop through and convert to DTO

     Each ticket can have many notes, so each note needs to be turned into a DTO as well.

    Refer to TicketMapper class for how tickets are mapped
     */
    public async Task<List<TicketResponseDto>> GetAllTicketsAsync()
    {
        var tickets = await _repo.GetAllTicketsAsync();

        return tickets.Select(TicketMapper.Format).ToList();
    }

    public async Task<TicketResponseDto?> GetTicketByIdAsync(int id)
    {
        var ticket = await _repo.GetTicketByIdAsync(id);

        if (ticket is null)
            throw new NullReferenceException("No id provided");

        return TicketMapper.Format(ticket);

    }

    public async Task<TicketResponseDto> CreateTicketAsync(NewTicketDTO newTicket)
    {
        var tkt = new Ticket
        {
            Description = newTicket.Description,
            Priority = newTicket.Priority,
            Status = "Open",
            CreatedAt = DateTime.Now
        };

        var createdTicket = await _repo.CreateTicketAsync(tkt);

        return TicketMapper.Format(createdTicket);


    }

    public async Task UpdateTicketStatusAsync(int id, UpdateTicketStatusDTO updateStatus)
    {

        if (updateStatus.Status != "Open" && updateStatus.Status != "Closed")
        {
            throw new ArgumentException("Status can only be 'Open' or 'Closed'");
        }

        await _repo.UpdateTicketStatusAsync(id, updateStatus);
    }

    public async Task AssignTechnicianAsync(AssignTicketDTO tick)
    {
        await _repo.AssignTechnicianAsync(tick);
    }

    public async Task<NoteResponseDto?> CreateNoteAsync(int id, CreateNoteDTO note)
    {
        var newNote = await _repo.CreateNoteAsync(id, note);

        return new NoteResponseDto
        {
            NoteId = newNote.NoteId,
            Content = newNote.Content,
            CreatedAt = newNote.CreatedAt,
            TicketId = newNote.TicketId
        };
    }

    public async Task DeleteTicketAsync(int ticketId)
    {
        if (ticketId <= 0)
            throw new ArgumentOutOfRangeException("Id must be greater than 0");

        Ticket ticket = await _repo.GetTicketByIdAsync(ticketId);

        if (ticket is null)
            throw new KeyNotFoundException("Ticket not found");

        await _repo.DeleteTicketAsync(ticket);
    }

}