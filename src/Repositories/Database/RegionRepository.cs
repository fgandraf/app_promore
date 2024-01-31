using Microsoft.EntityFrameworkCore;
using PromoreApi.Data;
using PromoreApi.Entities;
using PromoreApi.Models.InputModels;
using PromoreApi.Models.ViewModels;
using PromoreApi.Repositories.Contracts;

namespace PromoreApi.Repositories.Database;

public class RegionRepository :IRegionRepository
{
    private PromoreDataContext _context;

    public RegionRepository(PromoreDataContext context)
        => _context = context;
    
    public async Task<List<RegionView>> GetAll()
    {
        var regions = await _context
            .Regions
            .AsNoTracking()
            .Select(region => new RegionView
            {
                Id = region.Id,
                Name = region.Name,
                EstablishedDate = region.EstablishedDate,
                StartDate = region.StartDate,
                EndDate = region.EndDate,
                Lots = region.Lots.Select(x => x.Id).ToList()
            })
            .ToListAsync();

        return regions;
    }

    public async Task<RegionView> GetByIdAsync(int id)
    {
        var region = await _context
            .Regions
            .AsNoTracking()
            .Select(region => new RegionView
            {
                Id = region.Id,
                Name = region.Name,
                EstablishedDate = region.EstablishedDate,
                StartDate = region.StartDate,
                EndDate = region.EndDate,
                Lots = region.Lots.Select(x => x.Id).ToList()
            })
            .FirstOrDefaultAsync(x => x.Id == id);

        return region;
    }

    public async Task<long> InsertAsync(CreateRegionInput model)
    {
        var users = model.Users.Select(user => _context.Users.FirstOrDefault(x => x.Id == user)).ToList();

        var region = new Region
        {
            Name = model.Name,
            EstablishedDate = model.EstablishedDate,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            Users = users
        };
        
        _context.Regions.Add(region);
        await _context.SaveChangesAsync();
        return region.Id;
    }
    

    public async Task<bool> UpdateAsync(UpdateRegionInput model)
    {
        var region = await _context
            .Regions
            .FirstOrDefaultAsync(x => x.Id == model.Id);
        
        if (region is null)
            return false;

        region.Name = model.Name;
        region.EstablishedDate = model.EstablishedDate;
        region.StartDate = model.StartDate;
        region.EndDate = model.EndDate;
        
        _context.Update(region);
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var region = await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);
        if (region is null)
            return false;

        _context.Remove(region);
        await _context.SaveChangesAsync();

        return true;
    }
}