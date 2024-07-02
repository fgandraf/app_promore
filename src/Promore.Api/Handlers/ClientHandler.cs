using Microsoft.EntityFrameworkCore;
using Promore.Api.Data;
using Promore.Api.Data.Contexts;
using Promore.Core.Handlers;
using Promore.Core.Models;
using Promore.Core.Requests.Clients;
using Promore.Core.Responses;
using Promore.Core.Responses.Clients;

namespace Promore.Api.Handlers;

public class ClientHandler(PromoreDataContext context) : IClientHandler
{
    public async Task<Response<Client?>> CreateAsync(CreateClientRequest request)
    {
        try
        {
            var lot = await context
                .Lots
                .Include(x => x.User)
                .Include(x => x.Region)
                .Include(x => x.Clients)
                .FirstOrDefaultAsync(x => x.Id == request.LotId);
            
            if (lot is null)
                return new Response<Client?>(null, 404, "Lote não encontrado!");

            var client = new Client
            {
                Name = request.Name,
                Cpf = request.Cpf,
                Phone = request.Phone,
                MothersName = request.MothersName,
                BirthdayDate = request.BirthdayDate,
                Lot = lot
            };
    
            await context.Clients.AddAsync(client);
            await context.SaveChangesAsync();
            
            return new Response<Client?>(client, 201, "Cliente criado com sucesso!");
        }
        catch
        {
            return new Response<Client?>(null, 500, "[AHCCR12] Não foi possível criar o cliente!");
        }
       
    }

    public async Task<Response<Client?>> UpdateAsync(UpdateClientRequest request)
    {
        try
        {
            var client = await context
                .Clients
                .Where(x => x.Id == request.Id)
                .Include(lots => lots.Lot)
                .FirstOrDefaultAsync();
            if (client is null)
                return new Response<Client?>(null, 404, $"Cliente '{request.Name}' não encontrado!");
        
            var lot = await context
                .Lots
                .Include(x => x.User)
                .Include(x => x.Region)
                .Include(x => x.Clients)
                .FirstOrDefaultAsync(x => x.Id == request.LotId);
            if (lot is null)
                return new Response<Client?>(null, 404, $"Lote '{request.LotId}' não encontrado!");
        
            client.Name = request.Name;
            client.Cpf = request.Cpf;
            client.Phone = request.Phone;
            client.MothersName = request.MothersName;
            client.BirthdayDate = request.BirthdayDate;
            client.Lot = lot;
        
            context.Clients.Update(client);
            await context.SaveChangesAsync();
        
            return new Response<Client?>(client);
        }
        catch
        {
            return new Response<Client?>(null, 500, "[AHCUP12] Não foi possível alterar o cliente");
        }
        
    }
    
    public async Task<Response<Client?>> DeleteAsync(DeleteClientRequest request)
    {
        try
        {
            var client = await context.Clients.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (client is null)
                return new Response<Client?>(null, 404, $"Cliente '{request.Id}' não encontrado!");

            context.Clients.Remove(client);
            await context.SaveChangesAsync();
            
            return new Response<Client?>(null, 200, $"Cliente {request.Id} removido com sucesso!");
        }
        catch
        {
            return new Response<Client?>(null, 500, "[AHCDE12] Não foi possível excluir o cliente!");
        }
    }
    
    public async Task<Response<Client?>> GetByIdAsync(GetClientByIdRequest request)
    {
        try
        {
            var client = await context
                .Clients
                .AsNoTracking()
                .Where(x => x.Id == request.Id)
                .Include(lots => lots.Lot)
                .FirstOrDefaultAsync();
            
            return client is null 
                ? new Response<Client?>(null, 404, $"Cliente '{request.Id}' não encontrado!") 
                : new Response<Client?>(client);
        }
        catch
        {
            return new Response<Client?>(null, 500, "[AHCGT12] Não foi possível encontrar o cliente!");
        }
    }
    
    public async Task<Response<List<ClientResponse>?>> GetAllByLotIdAsync(GetAllClientsByLotIdRequest request)
    {
        var clients = await context
            .Clients
            .AsNoTracking()
            .Where(x => x.LotId == request.LotId)
            .Include(lots => lots.Lot)
            .Select(client => new ClientResponse
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
        
        return clients.Count == 0 
            ? new Response<List<ClientResponse>?>(null, 404, "Nenhum cliente cadastrado!") 
            : new Response<List<ClientResponse>?>(clients);
    }
    
    public async Task<PagedResponse<List<Client>>> GetAllAsync(GetAllClientsRequest request)
    {
        try
        {
            var query = context
                .Clients
                .AsNoTracking()
                .Include(lots => lots.Lot);
            
            var clients = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();

            return new PagedResponse<List<Client>>(
                data: clients, 
                totalCount: count, 
                currentPage: request.PageNumber, 
                pageSize: request.PageSize);
        }
        catch
        {
            return new PagedResponse<List<Client>>(null, 500, "[AHCGA12] Não foi possível encontrar o cliente!");
        }
    }
}