using Microsoft.EntityFrameworkCore;
using TechSupportSystem.Data;
using TechSupportSystem.Models;

public class TechnicianRepo : ITechnicianRepo
{
    private readonly AppDbContext _context;

    public TechnicianRepo(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Technician>> GetAllTechniciansAsync()
    {
        return await _context.Technicians.ToListAsync();
    }

    public async Task<Technician> CreateTechnicianAsync(Technician tech)
    {
        _context.Technicians.Add(tech);
        await _context.SaveChangesAsync();
        return tech;
    }

    public async Task DeleteTechnicianAsync(Technician tech)
    {
        _context.Technicians.Attach(tech);   
        _context.Technicians.Remove(tech);   
        await _context.SaveChangesAsync();
    }


}