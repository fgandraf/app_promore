using Microsoft.EntityFrameworkCore;
using Promore.Core.Contexts.Region.Contracts;
using Promore.Core.Contexts.Region.Entity;
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
    
    public async Task<Region> GetRegionByIdAsync(int id)
    {
        var region = await _context
            .Regions
            .AsNoTracking()
            .Include(region => region.Users)
            .Include(region => region.Lots)
            .FirstOrDefaultAsync(x => x.Id == id);
        
        return region;
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

    public async Task<long> InsertAsync(Region region)
    {
        _context.Regions.Add(region);
        return await _context.SaveChangesAsync();
    }
    

    public async Task<int> UpdateAsync(Region region)
    {
        _context.Update(region);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(int id)
    {
        var region = await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);
        if (region is null)
            return 0;

        _context.Remove(region);
        return await _context.SaveChangesAsync();
    }
}