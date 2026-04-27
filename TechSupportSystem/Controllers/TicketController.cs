using Microsoft.AspNetCore.Mvc;
using TechSupportSystem.Data;
using TechSupportSystem.Services;
using TechSupportSystem.DTOs;
using System.Data;
using TechSupportSystem.Models;



[Route("api/Ticket")]
[ApiController]

public class TicketController : ControllerBase
{
    private readonly ITicketService _ticketService;

    public TicketController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    //GET

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TicketResponseDto>>> GetTickets()
    {
        try
        {
            return await _ticketService.GetAllTicketsAsync();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("/api/Ticket/{ticketId}")]
    public async Task<ActionResult<TicketResponseDto?>> GetById(int ticketId)
    {
        try
        {
            return await _ticketService.GetTicketByIdAsync(ticketId);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    //POST
    [HttpPost]
    public async Task<ActionResult<TicketResponseDto>> CreateTicket(NewTicketDTO newTicket)
    {
        return await _ticketService.CreateTicketAsync(newTicket);
    }

    [HttpPost("/api/Ticket/assign")] 
    public async Task<ActionResult> AssignTicket(AssignTicketDTO tick)
    {
        await _ticketService.AssignTechnicianAsync(tick);
        return NoContent();
    }

    [HttpPost("/api/note/{ticketid}")]
    public async Task<ActionResult<NoteResponseDto?>> AddNote(int ticketid, CreateNoteDTO note)
    {
        return await _ticketService.CreateNoteAsync(ticketid, note);
    }


    //Patch
    [HttpPatch("/api/Ticket/status/{ticketid}")]
    public async Task<ActionResult> UpdateStatus(int ticketid, UpdateTicketStatusDTO updateStatus)
    {
        await _ticketService.UpdateTicketStatusAsync(ticketid, updateStatus);
        return NoContent();
    }

    //DELETE (tickets, tech from ticket, note from ticket)
    [HttpDelete("/api/Ticket{ticketId}")]
    public async Task<ActionResult> DeleteTicket(int ticketId)
    {
        await _ticketService.DeleteTicketAsync(ticketId);
        return NoContent();
    }
}
