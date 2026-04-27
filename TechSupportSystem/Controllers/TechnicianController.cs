using Microsoft.AspNetCore.Mvc;
using TechSupportSystem.DTOs;
using TechSupportSystem.Services;

[ApiController]
[Route("api/Technician")]
public class TechnicianController : ControllerBase
{
    private readonly ITechnicianService _technicianService;

    public TechnicianController(ITechnicianService technicianService)
    {
        _technicianService = technicianService;
    }

    [HttpGet]
    public async Task<ActionResult<List<TechnicianResponseDto>>> GetAllTechnicians()
    {
        try
        {
            return await _technicianService.GetAllTechniciansAsync();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }

    [HttpPost]
    public async Task<ActionResult> CreateTech(NewTechDTO technician)
    {
        var tech = await _technicianService.CreateTechnicianAsync(technician);
        return Ok(tech);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTech (int id)
    {
        await _technicianService.DeleteTechnicianAsync(id);
        return NoContent();
    }
}