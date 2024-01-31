using Microsoft.EntityFrameworkCore;
using PromoreApi.Data;
using PromoreApi.Entities;
using PromoreApi.Models.InputModels;
using PromoreApi.Models.ViewModels;
using PromoreApi.Repositories.Contracts;

namespace PromoreApi.Repositories.Database;

public class ProfessionalRepository : IProfessionalRepository
{
    private PromoreDataContext _context;

    public ProfessionalRepository(PromoreDataContext context)
        => _context = context;
    
    public async Task<List<ProfessionalView>> GetAll()
    {
        var professionals = await _context
            .Professionals
            .AsNoTracking()
            .Include(user => user.User)
            .Include(lots => lots.Lots)
            .Select(professional => new ProfessionalView()
            {
                Id = professional.Id,
                Name = professional.Name,
                Cpf = professional.Cpf,
                Profession = professional.Profession,
                UserId = professional.User.Id
            })
            .ToListAsync();
        
        return professionals;
    }

    public async Task<ProfessionalView> GetByIdAsync(int id)
    {
        var professional = await _context
            .Professionals
            .AsNoTracking()
            .Include(user => user.User)
            .Include(lots => lots.Lots)
            .Select(professional => new ProfessionalView
            {
                Id = professional.Id,
                Name = professional.Name,
                Cpf = professional.Cpf,
                Profession = professional.Profession,
                UserId = professional.User.Id
            })
            .FirstOrDefaultAsync(x => x.Id == id);
        
        return professional;
    }

    public async Task<long> InsertAsync(CreateProfessionalInput model)
    {
        var user = _context.Users.FirstOrDefault(x => x.Id == model.UserId);
        
        var professional = new Professional
        {
            Name = model.Name,
            Cpf = model.Cpf,
            Profession = model.Profession,
            User = user
        };
        
        _context.Professionals.Add(professional);
        await _context.SaveChangesAsync();
        return professional.Id;
    }

    public async Task<bool> UpdateAsync(UpdateProfessionalInput model)
    {
        var professional = await _context
            .Professionals
            .Include(x => x.User)
            .Include(x => x.Lots)
            .FirstOrDefaultAsync(x => x.Id == model.Id);
        
        if (professional is null)
            return false;

        professional.Name = model.Name;
        professional.Name = model.Name;
        professional.Cpf = model.Cpf;
        professional.Profession = model.Profession;
        
        _context.Update(professional);
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var professional = await _context.Professionals.FirstOrDefaultAsync(x => x.Id == id);
        if (professional is null)
            return false;

        _context.Remove(professional);
        await _context.SaveChangesAsync();

        return true;
    }
}