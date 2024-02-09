using Promore.Core;
using Promore.Core.Contracts;
using Promore.Core.ViewModels.Requests;

namespace Promore.UseCases.Lot.UseCases;

public class Update(ILotRepository lotRepository, IUserRepository userRepository, IRegionRepository regionRepository, IClientRepository clientRepository)
{
    public async Task<OperationResult> Handle(LotUpdateRequest model)
    {
        var lot = await lotRepository.GetLotById(model.Id);
        if (lot is null)
            return OperationResult.FailureResult($"Lote '{model.Id}' não encontrado!");

        var user = userRepository.GetEntityByIdAsync(model.UserId).Result;
        if (user is null)
            return OperationResult.FailureResult($"Usuário '{model.UserId}' não encontrado!");
        
        var region = regionRepository.GetRegionByIdAsync(model.RegionId).Result;
        if (region is null)
            return OperationResult.FailureResult($"Região '{model.RegionId}' não encontrada!");
        
        var clients = clientRepository.GetClientsByIdListAsync(model.Clients).Result;
        if (clients is null)
            return OperationResult.FailureResult($"Clientes não encontrados!");
        
        lot.Block = lot.Block;
        lot.Number = lot.Number;
        lot.SurveyDate = lot.SurveyDate;
        lot.LastModifiedDate = lot.LastModifiedDate;
        lot.Status = lot.Status;
        lot.Comments = lot.Comments;
        lot.User = user;
        lot.Region = region;
        lot.Clients = clients;
        
        var rowsAffected = await lotRepository.UpdateAsync(lot);

        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Não foi possível alterar o lote!");
    }
}