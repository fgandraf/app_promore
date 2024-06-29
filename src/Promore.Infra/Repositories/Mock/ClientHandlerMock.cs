using Promore.Core.Handlers;
using Promore.Core.Models;
using Promore.Core.Responses.Clients;

namespace Promore.Infra.Repositories.Mock;

public class ClientHandlerMock : IClientHandler
{
    private MockContext _context;

    public ClientHandlerMock(MockContext context)
        => _context = context;
    
    public Task<List<GetClientsResponse>> GetAll()
    {
        var clients = _context.Clients
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
            .ToList();
        
        return Task.FromResult(clients);
    }

    public Task<List<GetClientsByLotIdResponse>> GetAllByLotId(string lotId)
    {
        var client = _context.Clients
            .Where(x => x.LotId == lotId)
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
            .ToList();
        
        return Task.FromResult(client);
    }

    public Task<GetClientByIdResponse> GetByIdAsync(int id)
    {
        var client = _context.Clients
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
            .FirstOrDefault(x => x.Id == id);
        
        return Task.FromResult(client);
    }
    
    public Task<List<Client>> GetClientsByIdListAsync(List<int> clientsIds)
    {
        var clients = _context.Clients
            .Where(client => clientsIds.Contains(client.Id))
            .ToList();

        return Task.FromResult(clients);
    }
    
    public Task<Client> GetClientByIdAsync(int id)
    {
        var client = _context.Clients
            .FirstOrDefault(x => x.Id == id);
        
        return Task.FromResult(client);
    }

    public Task<long> InsertAsync(Client client)
    {
        client.Id = _context.Clients.Max(x => x.Id) + 1;
        _context.Clients.Add(client);
        return Task.FromResult(Convert.ToInt64(client.Id));
    }

    public Task<int> UpdateAsync(Client client)
    {
        var clientSaved = _context.Clients.FirstOrDefault(x => x.Id == client.Id);
        _context.Clients.Remove(clientSaved);
        _context.Clients.Add(client);
        return Task.FromResult(1);
    }

    public Task<int> DeleteAsync(int id)
    {
        var client = _context.Clients.FirstOrDefault(x => x.Id == id);
        if (client is null)
            return Task.FromResult(0);

        _context.Clients.Remove(client);
        return Task.FromResult(1);
    }
}