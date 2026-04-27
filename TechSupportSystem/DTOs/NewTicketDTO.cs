using System.ComponentModel.DataAnnotations;

namespace TechSupportSystem.DTOs;

public class NewTicketDTO
{
    [Required]
    public string Description {get; set;} = string.Empty;
    public string Priority {get; set;} = "Low";
}