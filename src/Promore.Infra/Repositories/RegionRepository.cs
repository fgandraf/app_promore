using Microsoft.EntityFrameworkCore;
using Promore.Core.Contracts;
using Promore.Core.Entities;
using Promore.Core.ViewModels.Responses;
using Promore.Infra.Data;

namespace Promore.Infra.Repositories;

public class RegionRepository :IRegionRepository
{
    private PromoreDataContext _context;

    public RegionRepository(PromoreDataContext context)
        => _context = context;
    
    public async Task<List<RegionResponse>> GetAll()
    {
        var regions = await _context
            .Regions
            .AsNoTracking()
            .Select(region => new RegionResponse
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

    public async Task<RegionResponse> GetByIdAsync(int id)
    {
        var region = await _context
            .Regions
            .AsNoTracking()
            .Select(region => new RegionResponse
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