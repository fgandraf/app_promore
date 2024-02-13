using Promore.Core.Contexts.LotContext.Contracts;
using Promore.Core.Contexts.LotContext.Entities;
using UseCases = Promore.Core.Contexts.LotContext.UseCases;

namespace Promore.Infra.Repositories.Mock;

public class LotRepositoryMock : ILotRepository
{
    private MockContext _context;

    public LotRepositoryMock(MockContext context)
        => _context = context;
    
    public Task<List<UseCases.GetStatusByRegion.Response>> GetStatusByRegion(int regionId)
    {
         var lots = _context.Lots
             .Where(x => x.RegionId == regionId)
             .Select(lot => new UseCases.GetStatusByRegion.Response
             (
                 lot.Id,
                 lot.Status,
                 lot.UserId
             ))
             .ToList();
        
         return Task.FromResult(lots);
    }
    
    public Task<Lot> GetLotById(string id)
    {
        var lot = _context.Lots
            .FirstOrDefault(x => x.Id == id);
        
        return Task.FromResult(lot);
    }
    
    public Task<UseCases.GetById.Response> GetByIdAsync(string id)
    {
        var clients = _context.Clients
            .Where(x => x.LotId == id)
            .Select(client => client.Id)
            .ToList();
        
        var lot = _context.Lots
            .Select(lot => new UseCases.GetById.Response
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
                clients
            ))
            .FirstOrDefault(x => x.Id == id);
        
        return Task.FromResult(lot);
    }

    public Task<string> InsertAsync(Lot lot)
    {
        _context.Lots.Add(lot);
        return Task.FromResult(lot.Id);
    }

    public Task<int> UpdateAsync(Lot lot)
    {
        var lotSaved = _context.Lots.FirstOrDefault(x => x.Id == lot.Id);
        _context.Lots.Remove(lotSaved);
        _context.Lots.Add(lot);
        return Task.FromResult(1);
    }

    public Task<int> DeleteAsync(string id)
    {
        var lot = _context.Lots.FirstOrDefault(x => x.Id == id);
        if (lot is null)
            return Task.FromResult(0);

        _context.Lots.Remove(lot);
        return Task.FromResult(1);
    }
}