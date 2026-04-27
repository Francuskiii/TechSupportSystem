
using TechSupportSystem.Models;
using TechSupportSystem.DTOs;

namespace TechSupportSystem.Services;



public interface ITechnicianService
{
    Task<List<TechnicianResponseDto>> GetAllTechniciansAsync();
    Task<Technician> CreateTechnicianAsync(NewTechDTO dto);
    Task DeleteTechnicianAsync(int id);

}