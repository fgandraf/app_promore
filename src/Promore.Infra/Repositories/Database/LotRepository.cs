using Microsoft.EntityFrameworkCore;
using Promore.Core.Handlers;
using Promore.Core.Models;
using Promore.Core.Responses.Lots;
using Promore.Infra.Data;

namespace Promore.Infra.Repositories.Database;

public class LotRepository : ILotHandler
{
    private PromoreDataContext _context;

    public LotRepository(PromoreDataContext context)
        => _context = context;
    
    public async Task<List<GetStatusByRegionResponse>> GetStatusByRegion(int regionId)
    {
         var lots = await _context
             .Lots
             .Include(x => x.Region)
             .Where(x => x.Region.Id == regionId)
             .AsNoTracking()
             .Select(lot => new GetStatusByRegionResponse
             (
                 lot.Id,
                 lot.Status,
                 lot.UserId
             ))
             .ToListAsync();
        
         return lots;
    }
    
    public async Task<Lot> GetLotById(string id)
    {
        var lot = await _context
            .Lots
            .Include(x => x.User)
            .Include(x => x.Region)
            .Include(x => x.Clients)
            .FirstOrDefaultAsync(x => x.Id == id);
        
        return lot;
    }
    
    public async Task<GetLotByIdResponse> GetByIdAsync(string id)
    {
        var lot = await _context
            .Lots
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Include(clients => clients.Clients)
            .Select(lot => new GetLotByIdResponse
            (
                lot.Id,
                lot.Block,
                lot.Number,
                lot.SurveyDate,
                lot.LastModifiedDate,
                lot.Status,
                lot.Comments,
                lot.UserId,
                lot.RegionId,
                lot.Clients.Select(x=> x.Id).ToList()
            ))
            .FirstOrDefaultAsync();
        
        return lot;
    }

    public async Task<string> InsertAsync(Lot lot)
    {
        _context.Lots.Add(lot);
        var rowsAffected = await _context.SaveChangesAsync();
        return rowsAffected > 0 ? lot.Id : string.Empty;
    }

    public async Task<int> UpdateAsync(Lot lot)
    {
        _context.Update(lot);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(string id)
    {
        var lot = await _context.Lots.FirstOrDefaultAsync(x => x.Id == id);
        if (lot is null)
            return 0;

        _context.Remove(lot);
        return await _context.SaveChangesAsync();
    }
    
}