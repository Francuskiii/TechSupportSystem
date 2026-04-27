using System.ComponentModel.DataAnnotations;

namespace TechSupportSystem.DTOs;

public class AssignTicketDTO
{
    public int TicketId {get; set;}
    public int TechnicianId {get; set;}
}