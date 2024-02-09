using Promore.Core.Contexts.ClientContext.Contracts;
using Promore.Core.Contexts.LotContext.Contracts;

namespace Promore.Core.Contexts.ClientContext.UseCases.Update;

public class UpdateHandler(IClientRepository clientRepository, ILotRepository lotRepository)
{
    
    public async Task<OperationResult> Handle(UpdateClientRequest model)
    {
        var client = await clientRepository.GetClientByIdAsync(model.Id);
        if (client is null)
            return OperationResult.FailureResult($"Cliente '{model.Name}' não encontrado!");
    
        var lot = await lotRepository.GetLotById(model.LotId);
        if (lot is null)
            return OperationResult.FailureResult($"Lote '{model.LotId}' não encontrado!");
        
        client.Name = model.Name;
        client.Cpf = model.Cpf;
        client.Phone = model.Phone;
        client.MothersName = model.MothersName;
        client.BirthdayDate = model.BirthdayDate;
        client.Lot = lot;
        
        var rowsAffected = await clientRepository.UpdateAsync(client);
    
        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Não foi possível alterar o cliente!");
    }
    
}