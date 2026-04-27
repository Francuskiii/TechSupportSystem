using Microsoft.EntityFrameworkCore;
using TechSupportSystem.DTOs;
using TechSupportSystem.Models;

namespace TechSupportSystem.Data;

public class TicketRepo : ITicketRepo
{
    private readonly AppDbContext _context;

    public TicketRepo(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Ticket>> GetAllTicketsAsync()
    {
        List<Ticket> result = await _context.Tickets
            .Include(t => t.Technicians)
            .Include(t => t.Notes)
            .ToListAsync();

        return result;
    }


    public async Task<Ticket?> GetTicketByIdAsync(int id)
    {
        return await _context.Tickets
            .Include(t => t.Technicians)
            .Include(t => t.Notes)
            .FirstOrDefaultAsync(t => t.TicketId == id);
    }

    public async Task<Ticket> CreateTicketAsync(Ticket ticket)
    {
        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();
        return ticket;
    }

    public async Task UpdateTicketStatusAsync(int id, UpdateTicketStatusDTO updateStatus)
    {
        //get ticket from db and verify it exists
        Ticket? ticket = await _context.Tickets
            .FirstOrDefaultAsync(t => t.TicketId == id);

        if (ticket is null)
            throw new KeyNotFoundException($"Ticket {id} not found.");

        //check if empty
        if (string.IsNullOrWhiteSpace(updateStatus.Status))
            throw new Exception("Status cannot be empty.");

        //update db & save
        ticket.Status = updateStatus.Status;
        await _context.SaveChangesAsync();
    }

    public async Task AssignTechnicianAsync(AssignTicketDTO tick)
    {
        //check to see if ticket exists
        Ticket? ticket = await _context.Tickets
            .Include(t => t.Technicians)
            .FirstOrDefaultAsync(t => t.TicketId == tick.TicketId);

        if (ticket is null)
            throw new KeyNotFoundException($"Ticket {tick.TicketId} not found");

        //check to see if technician exists
        Technician? tech = await _context.Technicians
            .FirstOrDefaultAsync(t => t.TechnicianId == tick.TechnicianId);

        if (tech is null)
            throw new KeyNotFoundException($"Technician {tick.TechnicianId} not found");

        //prevent duplicates
        if (ticket.Technicians.Any(t => t.TechnicianId == tick.TechnicianId))
            throw new Exception("Technician already assigned to this ticket.");

        //add relationship & save
        ticket.Technicians.Add(tech);
        await _context.SaveChangesAsync();
    }


    public async Task<Note> CreateNoteAsync(int ticketId, CreateNoteDTO dto)
    {
        var ticket = await _context.Tickets
            .FirstOrDefaultAsync(t => t.TicketId == ticketId);

        if (ticket == null)
            throw new KeyNotFoundException("Ticket not found");

        var note = new Note
        {
            TicketId = ticketId,
            Content = dto.Content,
            CreatedAt = DateTime.UtcNow
        };

        _context.Notes.Add(note);
        await _context.SaveChangesAsync();

        return note;
    }

    public async Task DeleteTicketAsync(Ticket ticketToDelete)
    {
        _context.Tickets.Remove(ticketToDelete);

        await _context.SaveChangesAsync();
    }


}