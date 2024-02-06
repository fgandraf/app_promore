using Promore.Core.Contexts.Client.Contracts;
using Promore.Core.Contexts.Lot.Contracts;
using Responses = Promore.Core.Contexts.Client.Models.Responses;
using Requests = Promore.Core.Contexts.Client.Models.Requests;

namespace Promore.Core.Contexts.Client;

public class ClientHandler
{
    private readonly IClientRepository _clientRepository;
    private readonly ILotRepository _lotRepository;

    public ClientHandler(IClientRepository clientRepository, ILotRepository lotRepository)
    {
        _clientRepository = clientRepository;
        _lotRepository = lotRepository;
    }
    
    public async Task<OperationResult<List<Responses.ReadClient>>> GetAllAsync()
    {
        var clients = await _clientRepository.GetAll();
        if (clients.Count == 0)
            return OperationResult<List<Responses.ReadClient>>.FailureResult("Nenhum cliente cadastrado!");
        
        return OperationResult<List<Responses.ReadClient>>.SuccessResult(clients);
    }
    
    public async Task<OperationResult<List<Responses.ReadClient>>> GetAllByLotIdAsync(string id)
    {
        var clients = await _clientRepository.GetAllByLotId(id);
        if (clients.Count == 0)
            return OperationResult<List<Responses.ReadClient>>.FailureResult("O lote não possui clientes cadastrados!");
        
        return OperationResult<List<Responses.ReadClient>>.SuccessResult(clients);
    }
    
    public async Task<OperationResult<Responses.ReadClient>> GetByIdAsync(int id)
    {
        var client = await _clientRepository.GetByIdAsync(id);
        if (client is null)
            return OperationResult<Responses.ReadClient>.FailureResult("Cliente não encontrado!");
        
        return OperationResult<Responses.ReadClient>.SuccessResult(client);
    }

    public async Task<OperationResult<long>> InsertAsync(Requests.CreateClient model)
    {
        var lot = _lotRepository.GetLotById(model.LotId).Result;
        
        var client = new Entity.Client
        {
            Name = model.Name,
            Cpf = model.Cpf,
            Phone = model.Phone,
            MothersName = model.MothersName,
            BirthdayDate = model.BirthdayDate,
            Lot = lot
        };

        var id = await _clientRepository.InsertAsync(client);

        return id > 0 ? OperationResult<long>.SuccessResult(id) : OperationResult<long>.FailureResult("Não foi possível inserir o cliente!");
    }
    
    
    public async Task<OperationResult> UpdateAsync(Requests.UpdateClient model)
    {
        var client = await _clientRepository.GetClientByIdAsync(model.Id);
        if (client is null)
            return OperationResult.FailureResult($"Cliente '{model.Name}' não encontrado!");

        var lot = await _lotRepository.GetLotById(model.LotId);
        if (lot is null)
            return OperationResult.FailureResult($"Lote '{model.LotId}' não encontrado!");
        
        client.Name = model.Name;
        client.Cpf = model.Cpf;
        client.Phone = model.Phone;
        client.MothersName = model.MothersName;
        client.BirthdayDate = model.BirthdayDate;
        client.Lot = lot;
        
        var rowsAffected = await _clientRepository.UpdateAsync(client);

        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Não foi possível alterar o cliente!");
    }
    
    public async Task<OperationResult> DeleteAsync(int id)
    {
        var rowsAffected = await _clientRepository.DeleteAsync(id);
        return rowsAffected > 0 ? OperationResult.SuccessResult("Cliente removido!") : OperationResult.FailureResult("Não foi possível apagar o cliente!");
    }

}