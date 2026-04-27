using System.ComponentModel.DataAnnotations;

namespace TechSupportSystem.DTOs;

public class UpdateTicketStatusDTO
{
    [Required]
    public string Status {get; set;} = string.Empty;
    
}
