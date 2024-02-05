using Microsoft.EntityFrameworkCore;
using Promore.Core.Contexts.Region.Contracts;
using Promore.Core.Contexts.Region.Entity;
using Promore.Core.Contexts.Region.Models.Requests;
using Promore.Core.Contexts.Region.Models.Responses;
using Promore.Infra.Data;

namespace Promore.Infra.Repositories;

public class RegionRepository :IRegionRepository
{
    private PromoreDataContext _context;

    public RegionRepository(PromoreDataContext context)
        => _context = context;
    
    public async Task<List<ReadRegion>> GetAll()
    {
        var regions = await _context
            .Regions
            .AsNoTracking()
            .Select(region => new ReadRegion
            {
                Id = region.Id,
                Name = region.Name,
                EstablishedDate = region.EstablishedDate,
                StartDate = region.StartDate,
                EndDate = region.EndDate,
            })
            .ToListAsync();

        return regions;
    }

    public async Task<List<Region>> GetRegionsByIdListAsync(List<int> regionsId)
    {
        var regions = await _context
            .Regions
            .AsNoTracking()
            .Where(region => regionsId.Contains(region.Id))
            .ToListAsync();

        return regions;
    }

    public async Task<ReadRegion> GetByIdAsync(int id)
    {
        var region = await _context
            .Regions
            .AsNoTracking()
            .Select(region => new ReadRegion
            {
                Id = region.Id,
                Name = region.Name,
                EstablishedDate = region.EstablishedDate,
                StartDate = region.StartDate,
                EndDate = region.EndDate,
            })
            .FirstOrDefaultAsync(x => x.Id == id);

        return region;
    }

    public async Task<long> InsertAsync(CreateRegion model)
    {
        var region = new Region
        {
            Name = model.Name,
            EstablishedDate = model.EstablishedDate,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            Users = model.Users.Select(user => _context.Users.FirstOrDefault(x => x.Id == user)).ToList()
        };
        
        _context.Regions.Add(region);
        await _context.SaveChangesAsync();
        return region.Id;
    }
    

    public async Task<bool> UpdateAsync(UpdateRegion model)
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