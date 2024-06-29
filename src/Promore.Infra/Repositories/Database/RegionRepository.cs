using Microsoft.EntityFrameworkCore;
using Promore.Core.Handlers;
using Promore.Core.Models;
using Promore.Core.Responses.Regions;
using Promore.Infra.Data;

namespace Promore.Infra.Repositories.Database;

public class RegionRepository :IRegionHandler
{
    private PromoreDataContext _context;

    public RegionRepository(PromoreDataContext context)
        => _context = context;
    
    public async Task<List<GetRegionsResponse>> GetAll()
    {
        var regions = await _context
            .Regions
            .AsNoTracking()
            .Select(region => new GetRegionsResponse
            (
                region.Id,
                region.Name,
                region.EstablishedDate,
                region.StartDate,
                region.EndDate
            ))
            .ToListAsync();

        return regions;
    }

    public async Task<List<Region>> GetRegionsByIdListAsync(List<int> regionsId)
    {
        var regions = await _context
            .Regions
            .Where(region => regionsId.Contains(region.Id))
            .ToListAsync();

        return regions;
    }
    
    public async Task<Region> GetRegionByIdAsync(int id)
    {
        var region = await _context
            .Regions
            //.Include(region => region.Users)
            //.Include(region => region.Lots)
            .FirstOrDefaultAsync(x => x.Id == id);
        
        return region;
    }

    public async Task<GetRegionsByIdResponse> GetByIdAsync(int id)
    {
        var region = await _context
            .Regions
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(region => new GetRegionsByIdResponse
            (
                region.Id,
                region.Name,
                region.EstablishedDate,
                region.StartDate,
                region.EndDate
            ))
            .FirstOrDefaultAsync();

        return region;
    }

    public async Task<long> InsertAsync(Region region)
    {
        _context.Regions.Add(region);
        var rowsAffected = await _context.SaveChangesAsync();
        return rowsAffected > 0 ? region.Id : 0;
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