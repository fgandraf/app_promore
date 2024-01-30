using Microsoft.EntityFrameworkCore;
using PromoreApi.Data;
using PromoreApi.Repositories.Contracts;
using PromoreApi.ViewModels;

namespace PromoreApi.Repositories.Database;

public class ProfessionalRepository : IProfessionalRepository
{
    private PromoreDataContext _context;

    public ProfessionalRepository(PromoreDataContext context)
        => _context = context;
    
    public async Task<IEnumerable<dynamic>> GetAll()
    {
        var professionals = await _context
            .Professionals
            .AsNoTracking()
            .Include(user => user.User)
            .Include(lots => lots.Lots)
            .Select(professional => new
            {
                Id = professional.Id,
                Name = professional.Name,
                Cpf = professional.Cpf,
                Profession = professional.Profession,
                User = new
                {
                    Id = professional.User.Id,
                    Active = professional.User.Active,
                    Email = professional.User.Email,
                    Roles = professional.User.Roles.Select(x => x.Id).ToList(),
                    Regions = professional.User.Regions.Select(x=> x.Id).ToList()
                }
            })
            .ToListAsync();
        
        return professionals;
    }

    public async Task<dynamic> GetByIdAsync(int id)
    {
        var professional = await _context
            .Professionals
            .AsNoTracking()
            .Include(user => user.User)
            .Include(lots => lots.Lots)
            .Select(professional => new
            {
                Id = professional.Id,
                Name = professional.Name,
                Cpf = professional.Cpf,
                Profession = professional.Profession,
                User = new
                {
                    Id = professional.User.Id,
                    Active = professional.User.Active,
                    Email = professional.User.Email,
                    Roles = professional.User.Roles.Select(x => x.Id).ToList(),
                    Regions = professional.User.Regions.Select(x=> x.Id).ToList()
                }
            })
            .FirstOrDefaultAsync(x => x.Id == id);
        
        return professional;
    }
    
}