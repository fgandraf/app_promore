using Microsoft.EntityFrameworkCore;
using Promore.Api.Data;
using Promore.Core;
using Promore.Core.Handlers;
using Promore.Core.Models;
using Promore.Core.Requests.Clients;
using Promore.Core.Responses;
using Promore.Core.Responses.Clients;

namespace Promore.Api.Handlers;

public class ClientHandler(PromoreDataContext context) : IClientHandler
{
    public async Task<OperationResult<List<GetClientsResponse>>> GetAllAsync(GetAllClientsRequest request)
    {
        var clients = await context
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
        
        if (clients.Count == 0)
            return OperationResult<List<GetClientsResponse>>.FailureResult("Nenhum cliente cadastrado!");
        
        return OperationResult<List<GetClientsResponse>>.SuccessResult(clients);
    }
    
    
    
    
    
    public async Task<Response<List<GetClientsResponse>>> GetAllAsync2(GetAllClientsRequest request)
    {
        var clients = await context
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
        
        if (clients.Count == 0)
            return new Response<List<GetClientsResponse>>(null, 404, "Nenhum cliente cadastrado!");
        
        return new Response<List<GetClientsResponse>>(clients, 201);
    }

    
    
    
    
    public async Task<OperationResult<List<GetClientsByLotIdResponse>>> GetAllByLotIdAsync(GetAllClientsByLotIdRequest request)
    {
        var clients = await context
            .Clients
            .AsNoTracking()
            .Where(x => x.LotId == request.LotId)
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
        
        if (clients.Count == 0)
            return OperationResult<List<GetClientsByLotIdResponse>>.FailureResult("O lote não possui clientes cadastrados!");
        
        return OperationResult<List<GetClientsByLotIdResponse>>.SuccessResult(clients);
    }
    
    public async Task<OperationResult<GetClientByIdResponse>> GetClientByIdAsync(GetClientByIdRequest request)
    {
        var client = await context
            .Clients
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
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
        
        if (client is null)
            return OperationResult<GetClientByIdResponse>.FailureResult("Cliente não encontrado!");
        
        return OperationResult<GetClientByIdResponse>.SuccessResult(client);
    }

    public async Task<OperationResult<long>> CreateAsync(CreateClientRequest request)
    {
        var lot = await context
            .Lots
            .Include(x => x.User)
            .Include(x => x.Region)
            .Include(x => x.Clients)
            .FirstOrDefaultAsync(x => x.Id == request.LotId);
        
        if (lot is null)
            return OperationResult<long>.FailureResult("Lote não encontrado!");

        var client = new Client
        {
            Name = request.Name,
            Cpf = request.Cpf,
            Phone = request.Phone,
            MothersName = request.MothersName,
            BirthdayDate = request.BirthdayDate,
            Lot = lot
        };
    
        context.Clients.Add(client);
        var rowsAffected = await context.SaveChangesAsync();
        var id = rowsAffected > 0 ? client.Id : 0;
    
        return id > 0 ? OperationResult<long>.SuccessResult(id) : OperationResult<long>.FailureResult("Não foi possível inserir o cliente!");
    }

    public async Task<OperationResult> UpdateAsync(UpdateClientRequest request)
    {
        var client = await context
            .Clients
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .Include(lots => lots.Lot)
            .FirstOrDefaultAsync();
        
        if (client is null)
            return OperationResult.FailureResult($"Cliente '{request.Name}' não encontrado!");
    
        
        var lot = await context
            .Lots
            .Include(x => x.User)
            .Include(x => x.Region)
            .Include(x => x.Clients)
            .FirstOrDefaultAsync(x => x.Id == request.LotId);
        
        if (lot is null)
            return OperationResult.FailureResult($"Lote '{request.LotId}' não encontrado!");
        
        client.Name = request.Name;
        client.Cpf = request.Cpf;
        client.Phone = request.Phone;
        client.MothersName = request.MothersName;
        client.BirthdayDate = request.BirthdayDate;
        client.Lot = lot;
        
        context.Update(client);
        var rowsAffected = await context.SaveChangesAsync();
    
        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Não foi possível alterar o cliente!");
    }
    
    public async Task<OperationResult> DeleteAsync(DeleteClientRequest request)
    {
        var client = await context.Clients.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (client is null)
            return OperationResult.FailureResult("Não foi possível apagar o cliente!");

        context.Remove(client);
        var rowsAffected = await context.SaveChangesAsync();
        
        return rowsAffected > 0 ? OperationResult.SuccessResult("Cliente removido!") : OperationResult.FailureResult("Não foi possível apagar o cliente!");
    }
}