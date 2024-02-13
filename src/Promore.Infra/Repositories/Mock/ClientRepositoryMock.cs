using Promore.Core.Contexts.ClientContext.Contracts;
using Promore.Core.Contexts.ClientContext.Entities;
using UseCases = Promore.Core.Contexts.ClientContext.UseCases;

namespace Promore.Infra.Repositories.Mock;

public class ClientRepositoryMock : IClientRepository
{
    private MockContext _context;

    public ClientRepositoryMock(MockContext context)
        => _context = context;
    
    public Task<List<UseCases.GetAll.Response>> GetAll()
    {
        var clients = _context.Clients
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
            .ToList();
        
        return Task.FromResult(clients);
    }

    public Task<List<UseCases.GetAllByLotId.Response>> GetAllByLotId(string lotId)
    {
        var client = _context.Clients
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
            .ToList();
        
        return Task.FromResult(client);
    }

    public Task<UseCases.GetById.Response> GetByIdAsync(int id)
    {
        var client = _context.Clients
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