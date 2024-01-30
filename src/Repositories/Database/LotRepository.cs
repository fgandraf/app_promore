using Microsoft.EntityFrameworkCore;
using PromoreApi.Data;
using PromoreApi.Repositories.Contracts;

namespace PromoreApi.Repositories.Database;

public class LotRepository : ILotRepository
{
    private PromoreDataContext _context;

    public LotRepository(PromoreDataContext context)
        => _context = context;
    
    public async Task<IEnumerable<dynamic>> GetAll()
    {
        var lots = await _context
            .Lots
            .AsNoTracking()
            .Include(professional => professional.Professional)
            .Include(region => region.Region)
            .Include(clients => clients.Clients)
            .Select(client => new
            {
                Id = client.Id,
                Block = client.Block,
                Number = client.Number,
                SurveyDate = client.SurveyDate,
                LastModifiedDate = client.LastModifiedDate,
                Status = client.Status,
                Comments = client.Comments,
                ProfessionalId = client.Professional.Id,
                RegionId = client.Region.Id,
                Clients = client.Clients.Select(x=> x.Id).ToList()
            })
            .ToListAsync();
        
        return lots;
    }

    public async Task<dynamic> GetByIdAsync(string id)
    {
        var lot = await _context
            .Lots
            .AsNoTracking()
            .Include(professional => professional.Professional)
            .Include(region => region.Region)
            .Include(clients => clients.Clients)
            .Select(client => new
            {
                Id = client.Id,
                Block = client.Block,
                Number = client.Number,
                SurveyDate = client.SurveyDate,
                LastModifiedDate = client.LastModifiedDate,
                Status = client.Status,
                Comments = client.Comments,
                ProfessionalId = client.Professional.Id,
                RegionId = client.Region.Id,
                Clients = client.Clients.Select(x=> x.Id).ToList()
            })
            .FirstOrDefaultAsync(x => x.Id == id);
        
        return lot;
    }
}