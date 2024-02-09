using Microsoft.EntityFrameworkCore;
using Promore.Core.Contracts;
using Promore.Core.Entities;
using Promore.Core.ViewModels.Responses;
using Promore.Infra.Data;

namespace Promore.Infra.Repositories;

public class LotRepository : ILotRepository
{
    private PromoreDataContext _context;

    public LotRepository(PromoreDataContext context)
        => _context = context;
    
    public async Task<List<LotStatusResponse>> GetStatusByRegion(int regionId)
    {
         var lots = await _context
             .Lots
             .Include(x => x.Region)
             .Where(x => x.Region.Id == regionId)
             .AsNoTracking()
             .Select(lot => new LotStatusResponse
             {
                 Id = lot.Id,
                 Status = lot.Status,
                 UserId = lot.UserId,
             })
             .ToListAsync();
        
         return lots;
    }
    
    public async Task<Lot> GetLotById(string id)
    {
        var lot = await _context
            .Lots
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
        
        return lot;
    }
    
    public async Task<LotResponse> GetByIdAsync(string id)
    {
        var lot = await _context
            .Lots
            .AsNoTracking()
            .Include(clients => clients.Clients)
            .Select(lot => new LotResponse
            {
                Id = lot.Id,
                Block = lot.Block,
                Number = lot.Number,
                SurveyDate = lot.SurveyDate,
                LastModifiedDate = lot.LastModifiedDate,
                Status = lot.Status,
                Comments = lot.Comments,
                UserId = lot.UserId,
                RegionId = lot.RegionId,
                Clients = lot.Clients.Select(x=> x.Id).ToList()
            })
            .FirstOrDefaultAsync(x => x.Id == id);
        
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