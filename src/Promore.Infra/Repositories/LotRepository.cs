using Microsoft.EntityFrameworkCore;
using Promore.Core.Contexts.Lot.Contracts;
using Promore.Core.Contexts.Lot.Entity;
using Promore.Core.Contexts.Lot.Models.Requests;
using Promore.Core.Contexts.Lot.Models.Responses;
using Promore.Infra.Data;

namespace Promore.Infra.Repositories;

public class LotRepository : ILotRepository
{
    private PromoreDataContext _context;

    public LotRepository(PromoreDataContext context)
        => _context = context;
    
    public async Task<List<ReadLot>> GetAll()
    {
        var lots = await _context
            .Lots
            .AsNoTracking()
            .Include(professional => professional.User)
            .Include(region => region.Region)
            .Include(clients => clients.Clients)
            .Select(lot => new ReadLot
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
            .ToListAsync();
        
        return lots;
    }

    public async Task<List<ReadStatusLot>> GetStatusByRegion(int regionId)
    {
         var lots = await _context
             .Lots
             .Include(x => x.Region)
             .Where(x => x.Region.Id == regionId)
             .AsNoTracking()
             .Select(lot => new ReadStatusLot
             {
                 Id = lot.Id,
                 Status = lot.Status,
                 UserId = lot.UserId,
             })
             .ToListAsync();
        
         return lots;
    }

    public async Task<ReadLot> GetByIdAsync(string id)
    {
        var lot = await _context
            .Lots
            .AsNoTracking()
            .Include(clients => clients.Clients)
            .Select(lot => new ReadLot
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

    public async Task<string> InsertAsync(CreateLot model)
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

    public async Task<bool> UpdateAsync(UpdateLot model)
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