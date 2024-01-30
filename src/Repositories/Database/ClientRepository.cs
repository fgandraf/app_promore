using Microsoft.EntityFrameworkCore;
using PromoreApi.Data;
using PromoreApi.Repositories.Contracts;

namespace PromoreApi.Repositories.Database;

public class ClientRepository : IClientRepository
{
    private PromoreDataContext _context;

    public ClientRepository(PromoreDataContext context)
        => _context = context;
    
    public async Task<IEnumerable<dynamic>> GetAll()
    {
        var clients = await _context
            .Clients
            .AsNoTracking()
            .Include(lots => lots.Lot)
            .Select(client => new
            {
                Id = client.Id,
                Name = client.Name,
                Cpf = client.Cpf,
                Phone = client.Phone,
                MothersName = client.MothersName,
                BirthdayDate = client.BirthdayDate,
                LotId = client.Lot.Id
            })
            .ToListAsync();
        
        return clients;
    }

    public async Task<dynamic> GetByIdAsync(int id)
    {
        var client = await _context
            .Clients
            .AsNoTracking()
            .Include(lots => lots.Lot)
            .Select(client => new
            {
                Id = client.Id,
                Name = client.Name,
                Cpf = client.Cpf,
                Phone = client.Phone,
                MothersName = client.MothersName,
                BirthdayDate = client.BirthdayDate,
                LotId = client.Lot.Id
            })
            .FirstOrDefaultAsync(x => x.Id == id);
        
        return client;
    }
}