using Microsoft.EntityFrameworkCore;
using PromoreApi.Data;
using PromoreApi.Entities;
using PromoreApi.Repositories.Contracts;

namespace PromoreApi.Repositories.Database;

public class RegionRepository :IRegionRepository
{
    private PromoreDataContext _context;

    public RegionRepository(PromoreDataContext context)
        => _context = context;
    
    public async Task<List<Region>> GetAll()
    {
        var regions = await _context
            .Regions
            .AsNoTracking()
            .ToListAsync();

        return regions;
    }

    public async Task<Region> GetByIdAsync(int id)
    {
        var region = await _context
            .Regions
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        return region;
    }
}