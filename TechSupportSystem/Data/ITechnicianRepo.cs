using TechSupportSystem.Models;

public interface ITechnicianRepo
{
    Task<List<Technician>> GetAllTechniciansAsync();
    Task<Technician> CreateTechnicianAsync(Technician tech);

    Task DeleteTechnicianAsync(Technician tech);
    
}