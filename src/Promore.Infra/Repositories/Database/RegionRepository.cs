using Microsoft.EntityFrameworkCore;
using Promore.Core.Contexts.RegionContext.Contracts;
using Promore.Core.Contexts.RegionContext.Entities;
using Promore.Infra.Data;
using UseCases = Promore.Core.Contexts.RegionContext.UseCases;

namespace Promore.Infra.Repositories.Database;

public class RegionRepository :IRegionRepository
{
    private PromoreDataContext _context;

    public RegionRepository(PromoreDataContext context)
        => _context = context;
    
    public async Task<List<UseCases.GetAll.Response>> GetAll()
    {
        var regions = await _context
            .Regions
            .AsNoTracking()
            .Select(region => new UseCases.GetAll.Response
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

    public async Task<UseCases.GetById.Response> GetByIdAsync(int id)
    {
        var region = await _context
            .Regions
            .AsNoTracking()
            .Select(region => new UseCases.GetById.Response
            (
                region.Id,
                region.Name,
                region.EstablishedDate,
                region.StartDate,
                region.EndDate
            ))
            .FirstOrDefaultAsync(x => x.Id == id);

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