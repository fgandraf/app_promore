using Microsoft.EntityFrameworkCore;
using Promore.Core.Handlers;
using Promore.Core.Models;
using Promore.Core.Responses.Clients;
using Promore.Infra.Data;

namespace Promore.Infra.Repositories.Database;

public class ClientRepository : IClientHandler
{
    private readonly PromoreDataContext _context;

    public ClientRepository(PromoreDataContext context)
        => _context = context;
    
    public async Task<List<GetClientsResponse>> GetAll()
    {
        var clients = await _context
            .Clients
            .AsNoTracking()
            .Include(lots => lots.Lot)
            .Select(client => new GetClientsResponse
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

    public async Task<List<GetClientsByLotIdResponse>> GetAllByLotId(string lotId)
    {
        var client = await _context
            .Clients
            .AsNoTracking()
            .Where(x => x.LotId == lotId)
            .Include(lots => lots.Lot)
            .Select(client => new GetClientsByLotIdResponse
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

    public async Task<GetClientByIdResponse> GetByIdAsync(int id)
    {
        var client = await _context
            .Clients
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Include(lots => lots.Lot)
            .Select(client => new GetClientByIdResponse
            (
                client.Id,
                client.Name,
                client.Cpf,
                client.Phone,
                client.MothersName,
                client.BirthdayDate,
                client.LotId
            ))
            .FirstOrDefaultAsync();
        
        return client;
    }
    
    public async Task<List<Client>> GetClientsByIdListAsync(List<int> clientsIds)
    {
        var clients = await _context
            .Clients
            .Where(client => clientsIds.Contains(client.Id))
            .ToListAsync();

        return clients;
    }
    
    public async Task<Client> GetClientByIdAsync(int id)
    {
        var client = await _context
            .Clients
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