using Promore.Core;
using Promore.Core.Handlers;
using Promore.Core.Models;
using Promore.Core.Requests.Clients;
using Promore.Core.Responses.Clients;

namespace Promore.Api.Services;

public class ClientService(IClientHandler clientHandler, ILotHandler lotHandler)
{
    public async Task<OperationResult> DeleteAsync(int id)
    {
        var rowsAffected = await clientHandler.DeleteAsync(id);
        return rowsAffected > 0 ? OperationResult.SuccessResult("Cliente removido!") : OperationResult.FailureResult("Não foi possível apagar o cliente!");
    }
    
    public async Task<OperationResult<List<GetClientsResponse>>> GetAllAsync()
    {
        var clients = await clientHandler.GetAll();
        if (clients.Count == 0)
            return OperationResult<List<GetClientsResponse>>.FailureResult("Nenhum cliente cadastrado!");
        
        return OperationResult<List<GetClientsResponse>>.SuccessResult(clients);
    }

    public async Task<OperationResult<List<GetClientsByLotIdResponse>>> GetAllByLotIdAsync(string id)
    {
        var clients = await clientHandler.GetAllByLotId(id);
        if (clients.Count == 0)
            return OperationResult<List<GetClientsByLotIdResponse>>.FailureResult("O lote não possui clientes cadastrados!");
        
        return OperationResult<List<GetClientsByLotIdResponse>>.SuccessResult(clients);
    }

    public async Task<OperationResult<GetClientByIdResponse>> GetByIdAsync(int id)
    {
        var client = await clientHandler.GetByIdAsync(id);
        if (client is null)
            return OperationResult<GetClientByIdResponse>.FailureResult("Cliente não encontrado!");
        
        return OperationResult<GetClientByIdResponse>.SuccessResult(client);
    }

    public async Task<OperationResult<long>> CreateAsync(CreateClientRequest model)
    {
        var lot = lotHandler.GetLotById(model.LotId).Result;
        if (lot is null)
            return OperationResult<long>.FailureResult("Lote não encontrado!");

        var client = new Client
        {
            Name = model.Name,
            Cpf = model.Cpf,
            Phone = model.Phone,
            MothersName = model.MothersName,
            BirthdayDate = model.BirthdayDate,
            Lot = lot
        };
    
        var id = await clientHandler.InsertAsync(client);
    
        return id > 0 ? OperationResult<long>.SuccessResult(id) : OperationResult<long>.FailureResult("Não foi possível inserir o cliente!");
    }

    public async Task<OperationResult> UpdateAsync(UpdateClientRequest model)
    {
        var client = await clientHandler.GetClientByIdAsync(model.Id);
        if (client is null)
            return OperationResult.FailureResult($"Cliente '{model.Name}' não encontrado!");
    
        var lot = await lotHandler.GetLotById(model.LotId);
        if (lot is null)
            return OperationResult.FailureResult($"Lote '{model.LotId}' não encontrado!");
        
        client.Name = model.Name;
        client.Cpf = model.Cpf;
        client.Phone = model.Phone;
        client.MothersName = model.MothersName;
        client.BirthdayDate = model.BirthdayDate;
        client.Lot = lot;
        
        var rowsAffected = await clientHandler.UpdateAsync(client);
    
        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Não foi possível alterar o cliente!");
    }
}