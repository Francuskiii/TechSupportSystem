using System.ComponentModel.DataAnnotations;

namespace TechSupportSystem.DTOs;

public class CreateNoteDTO
{
    public string Content { get; set; } = string.Empty;
}