using Microsoft.EntityFrameworkCore;
using Promore.Core.Contexts.ClientContext.Contracts;
using Promore.Core.Contexts.ClientContext.Entities;
using Promore.Infra.Data;
using UseCases = Promore.Core.Contexts.ClientContext.UseCases;

namespace Promore.Infra.Repositories.Database;

public class ClientRepository : IClientRepository
{
    private readonly PromoreDataContext _context;

    public ClientRepository(PromoreDataContext context)
        => _context = context;
    
    public async Task<List<UseCases.GetAll.Response>> GetAll()
    {
        var clients = await _context
            .Clients
            .AsNoTracking()
            .Include(lots => lots.Lot)
            .Select(client => new UseCases.GetAll.Response
            (
                client.Id,
                client.Name,
                client.Cpf,
                client.Phone,
                client.MothersName,
                client.BirthdayDate,
                client.LotId
            ))
            .ToListAsync();
        
        return clients;
    }

    public async Task<List<UseCases.GetAllByLotId.Response>> GetAllByLotId(string lotId)
    {
        var client = await _context
            .Clients
            .AsNoTracking()
            .Include(lots => lots.Lot)
            .Where(x => x.LotId == lotId)
            .Select(client => new UseCases.GetAllByLotId.Response
            (
                client.Id,
                client.Name,
                client.Cpf,
                client.Phone,
                client.MothersName,
                client.BirthdayDate,
                client.LotId
            ))
            .ToListAsync();
        
        return client;
    }

    public async Task<UseCases.GetById.Response> GetByIdAsync(int id)
    {
        var client = await _context
            .Clients
            .AsNoTracking()
            .Include(lots => lots.Lot)
            .Select(client => new UseCases.GetById.Response
            (
                client.Id,
                client.Name,
                client.Cpf,
                client.Phone,
                client.MothersName,
                client.BirthdayDate,
                client.LotId
            ))
            .FirstOrDefaultAsync(x => x.Id == id);
        
        return client;
    }
    
    public async Task<List<Client>> GetClientsByIdListAsync(List<int> clientsIds)
    {
        var clients = await _context
            .Clients
            .AsNoTracking()
            .Where(client => clientsIds.Contains(client.Id))
            .ToListAsync();

        return clients;
    }
    
    public async Task<Client> GetClientByIdAsync(int id)
    {
        var client = await _context
            .Clients
            .AsNoTracking()
            .Include(lots => lots.Lot)
            .FirstOrDefaultAsync(x => x.Id == id);
        
        return client;
    }

    public async Task<long> InsertAsync(Client client)
    {
        _context.Clients.Add(client);
        var rowsAffected = await _context.SaveChangesAsync();
        return rowsAffected > 0 ? client.Id : 0;
    }

    public async Task<int> UpdateAsync(Client client)
    {
        _context.Update(client);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(int id)
    {
        var client = await _context.Clients.FirstOrDefaultAsync(x => x.Id == id);
        if (client is null)
            return 0;

        _context.Remove(client);
        return await _context.SaveChangesAsync();
    }
    
}