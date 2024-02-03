using Microsoft.EntityFrameworkCore;
using Promore.Infra.Data;
using Promore.Core.Entities;
using Promore.Core.Models.InputModels;
using Promore.Core.Models.ViewModels;
using Promore.Core.Repositories.Contracts;

namespace Promore.Infra.Repositories.Database;

public class ClientRepository : IClientRepository
{
    private PromoreDataContext _context;

    public ClientRepository(PromoreDataContext context)
        => _context = context;
    
    public async Task<List<ClientView>> GetAll()
    {
        var clients = await _context
            .Clients
            .AsNoTracking()
            .Include(lots => lots.Lot)
            .Select(client => new ClientView
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

    public async Task<ClientView> GetByIdAsync(int id)
    {
        var client = await _context
            .Clients
            .AsNoTracking()
            .Include(lots => lots.Lot)
            .Select(client => new ClientView
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

    public async Task<long> InsertAsync(CreateClientInput model)
    {
        var client = new Client
        {
            Name = model.Name,
            Cpf = model.Cpf,
            Phone = model.Phone,
            MothersName = model.MothersName,
            BirthdayDate = model.BirthdayDate,
            Lot = _context.Lots.FirstOrDefault(x => x.Id == model.LotId)
        };
        
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();
        
        return client.Id;
    }

    public async Task<bool> UpdateAsync(UpdateClientInput model)
    {
        var client = await _context
            .Clients
            .FirstOrDefaultAsync(x => x.Id == model.Id);
        
        if (client is null)
            return false;

        client.Name = model.Name;
        client.Cpf = model.Cpf;
        client.Phone = model.Phone;
        client.MothersName = model.MothersName;
        client.BirthdayDate = model.BirthdayDate;
        client.Lot = _context.Lots.FirstOrDefault(x => x.Id == model.LotId);
        
        _context.Update(client);
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var client = await _context.Clients.FirstOrDefaultAsync(x => x.Id == id);
        if (client is null)
            return false;

        _context.Remove(client);
        await _context.SaveChangesAsync();

        return true;
    }
}