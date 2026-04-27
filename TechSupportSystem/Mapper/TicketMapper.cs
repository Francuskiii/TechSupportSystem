
using TechSupportSystem.DTOs;
using TechSupportSystem.Models;

namespace TechSupportSystem.Mapper;


//mapper class that automatically formats Ticket DTO so i dont need to rewrite code lol
//yes i know i can use automapper but i forgot about it after iwrote it
//next steps - turn into abstract class so i can map tickets and notes seperatly
public static class TicketMapper
{
    public static TicketResponseDto Format(Ticket ticket)
    {
        return new TicketResponseDto
        {
            TicketId = ticket.TicketId,
            Description = ticket.Description,
            Priority = ticket.Priority,
            Status = ticket.Status,
            CreatedAt = ticket.CreatedAt,

            Technicians = ticket.Technicians.Select(t => new TechnicianDto
            {
                TechnicianId = t.TechnicianId,
                Name = t.Name,
                Level = t.Level
            }).ToList(),

            Notes = ticket.Notes.Select(n => new NoteDto
            {
                NoteId = n.NoteId,
                Content = n.Content,
                CreatedAt = n.CreatedAt
            }).ToList()
        };
    }
}