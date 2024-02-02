using Microsoft.EntityFrameworkCore;
using PromoreApi.Data;
using PromoreApi.Entities;
using PromoreApi.Models.InputModels;
using PromoreApi.Models.ViewModels;
using PromoreApi.Repositories.Contracts;

namespace PromoreApi.Repositories.Database;

public class LotRepository : ILotRepository
{
    private PromoreDataContext _context;

    public LotRepository(PromoreDataContext context)
        => _context = context;
    
    public async Task<List<LotView>> GetAll()
    {
        var lots = await _context
            .Lots
            .AsNoTracking()
            .Include(professional => professional.User)
            .Include(region => region.Region)
            .Include(clients => clients.Clients)
            .Select(lot => new LotView
            {
                Id = lot.Id,
                Block = lot.Block,
                Number = lot.Number,
                SurveyDate = lot.SurveyDate,
                LastModifiedDate = lot.LastModifiedDate,
                Status = lot.Status,
                Comments = lot.Comments,
                UserId = lot.User.Id,
                RegionId = lot.Region.Id,
                Clients = lot.Clients.Select(x=> x.Id).ToList()
            })
            .ToListAsync();
        
        return lots;
    }

    public async Task<List<LotStatusView>> GetStatusByRegion(int regionId)
    {
         var lots = await _context
             .Lots
             .Include(x => x.Region)
             .Where(x => x.Region.Id == regionId)
             .Include(x => x.User)
             .AsNoTracking()
             .Select(lot => new LotStatusView
             {
                 Id = lot.Id,
                 Status = lot.Status,
                 UserId = lot.User.Id
             })
             .ToListAsync();
        
         return lots;
    }

    public async Task<LotView> GetByIdAsync(string id)
    {
        var lot = await _context
            .Lots
            .AsNoTracking()
            .Include(professional => professional.User)
            .Include(region => region.Region)
            .Include(clients => clients.Clients)
            .Select(client => new LotView
            {
                Id = client.Id,
                Block = client.Block,
                Number = client.Number,
                SurveyDate = client.SurveyDate,
                LastModifiedDate = client.LastModifiedDate,
                Status = client.Status,
                Comments = client.Comments,
                UserId = client.User.Id,
                RegionId = client.Region.Id,
                Clients = client.Clients.Select(x=> x.Id).ToList()
            })
            .FirstOrDefaultAsync(x => x.Id == id);
        
        return lot;
    }

    public async Task<string> InsertAsync(CreateLotInput model)
    {
        var lot = new Lot
        {
            Id = model.Id,
            Block = model.Block,
            Number = model.Number,
            SurveyDate = model.SurveyDate,
            LastModifiedDate = model.LastModifiedDate,
            Status = model.Status,
            Comments = model.Comments,
            User = _context.Users.FirstOrDefault(x => x.Id == model.UserId),
            Region = _context.Regions.FirstOrDefault(x => x.Id == model.RegionId),
            Clients = model.Clients.Select(client => _context.Clients.FirstOrDefault(x => x.Id == client)).ToList()
        };
        
        _context.Lots.Add(lot);
        await _context.SaveChangesAsync();
        
        return lot.Id;
    }

    public async Task<bool> UpdateAsync(UpdateLotInput model)
    {
        var lot = await _context
            .Lots
            .FirstOrDefaultAsync(x => x.Id == model.Id);
        
        if (lot is null)
            return false;

        lot.Block = model.Block;
        lot.Number = model.Number;
        lot.SurveyDate = model.SurveyDate;
        lot.LastModifiedDate = model.LastModifiedDate;
        lot.Status = model.Status;
        lot.Comments = model.Comments;
        lot.User = _context.Users.FirstOrDefault(x => x.Id == model.UserId);
        lot.Region = _context.Regions.FirstOrDefault(x => x.Id == model.RegionId);
        lot.Clients = model.Clients.Select(client => _context.Clients.FirstOrDefault(x => x.Id == client)).ToList();
        
        _context.Update(lot);
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var lot = await _context.Lots.FirstOrDefaultAsync(x => x.Id == id);
        if (lot is null)
            return false;

        _context.Remove(lot);
        await _context.SaveChangesAsync();

        return true;
    }
}