using Promore.Core.Contexts.ClientContext.Contracts;
using Promore.Core.Contexts.LotContext.Contracts;
using Promore.Core.Contexts.RegionContext.Contracts;
using Promore.Core.Contexts.UserContext.Contracts;

namespace Promore.Core.Contexts.LotContext.UseCases.Update;

public class Handler(ILotRepository lotRepository, IUserRepository userRepository, IRegionRepository regionRepository, IClientRepository clientRepository)
{
    public async Task<OperationResult> Handle(UpdateLotRequest model)
    {
        var lot = await lotRepository.GetLotById(model.Id);
        if (lot is null)
            return OperationResult.FailureResult($"Lote '{model.Id}' não encontrado!");

        var user = userRepository.GetUserByIdAsync(model.UserId).Result;
        if (user is null)
            return OperationResult.FailureResult($"Usuário '{model.UserId}' não encontrado!");
        
        var region = regionRepository.GetRegionByIdAsync(model.RegionId).Result;
        if (region is null)
            return OperationResult.FailureResult($"Região '{model.RegionId}' não encontrada!");
        
        var clients = clientRepository.GetClientsByIdListAsync(model.Clients).Result;
        if (clients is null)
            return OperationResult.FailureResult($"Clientes não encontrados!");
        
        lot.Block = new string(model.Id.Where(char.IsLetter).ToArray());
        lot.Number = Convert.ToInt32(new string(model.Id.Where(char.IsDigit).ToArray()));
        lot.SurveyDate = model.SurveyDate;
        lot.LastModifiedDate = model.LastModifiedDate;
        lot.Status = model.Status;
        lot.Comments = model.Comments;
        lot.User = user;
        lot.Region = region;
        lot.Clients = clients;
        
        var rowsAffected = await lotRepository.UpdateAsync(lot);

        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Não foi possível alterar o lote!");
    }
}