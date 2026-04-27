using System.Security.Principal;
using TechSupportSystem.Data;
using TechSupportSystem.DTOs;
using TechSupportSystem.Mapper;
using TechSupportSystem.Models;

namespace TechSupportSystem.Services;


/* TODO:

- get tech by id
- better delete validation

 */



public class TechnicianService : ITechnicianService
{
    private readonly ITechnicianRepo _repo;

    public TechnicianService(ITechnicianRepo repo)
    {
        _repo = repo;
    }

    public async Task<List<TechnicianResponseDto>> GetAllTechniciansAsync()
    {
        var techs = await _repo.GetAllTechniciansAsync();

        return techs.Select(t => new TechnicianResponseDto
        {
            TechnicianId = t.TechnicianId,
            Name = t.Name,
            Level = t.Level
        }).ToList();
    }

    

    public async Task<Technician> CreateTechnicianAsync(NewTechDTO dto)
    {
        var tech = new Technician { Name = dto.Name, Level = dto.Level };

        return await _repo.CreateTechnicianAsync(tech);
    }

    
    public async Task DeleteTechnicianAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentOutOfRangeException("Id must be greater than 0");

        var tech = new Technician { TechnicianId = id };

        await _repo.DeleteTechnicianAsync(tech);
    }

}