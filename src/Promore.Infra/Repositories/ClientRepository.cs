using Microsoft.EntityFrameworkCore;
using Promore.Core.Contexts.Client.Contracts;
using Promore.Core.Contexts.Client.Entity;
using Promore.Core.Contexts.Client.Models.Requests;
using Promore.Core.Contexts.Client.Models.Responses;
using Promore.Infra.Data;

namespace Promore.Infra.Repositories;

public class ClientRepository : IClientRepository
{
    private PromoreDataContext _context;

    public ClientRepository(PromoreDataContext context)
        => _context = context;
    
    public async Task<List<ReadClient>> GetAll()
    {
        var clients = await _context
            .Clients
            .AsNoTracking()
            .Include(lots => lots.Lot)
            .Select(client => new ReadClient
            {
                Id = client.Id,
                Name = client.Name,
                Cpf = client.Cpf,
                Phone = client.Phone,
                MothersName = client.MothersName,
                BirthdayDate = client.BirthdayDate,
                LotId = client.LotId
            })
            .ToListAsync();
        
        return clients;
    }

    public async Task<ReadClient> GetByIdAsync(int id)
    {
        var client = await _context
            .Clients
            .AsNoTracking()
            .Include(lots => lots.Lot)
            .Select(client => new ReadClient
            {
                Id = client.Id,
                Name = client.Name,
                Cpf = client.Cpf,
                Phone = client.Phone,
                MothersName = client.MothersName,
                BirthdayDate = client.BirthdayDate,
                LotId = client.LotId
            })
            .FirstOrDefaultAsync(x => x.Id == id);
        
        return client;
    }

    public async Task<long> InsertAsync(CreateClient model)
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

    public async Task<bool> UpdateAsync(UpdateClient model)
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