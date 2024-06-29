using Promore.Core;
using Promore.Core.Handlers;
using Promore.Core.Models;
using Promore.Core.Requests.Lots;
using Promore.Core.Responses.Lots;

namespace Promore.Api.Services;

public class LotService(
    ILotHandler lotHandler,
    IRegionHandler regionHandler,
    IClientHandler clientHandler,
    IUserHandler userHandler)
{
    public async Task<OperationResult<List<GetStatusByRegionResponse>>> GetStatusByRegionAsync(int regionId)
    {
        var regions = await lotHandler.GetStatusByRegion(regionId);
        return OperationResult<List<GetStatusByRegionResponse>>.SuccessResult(regions);
    }

    public async Task<OperationResult<GetLotByIdResponse>> GetByIdAsync(string id)
    {
        var lot = await lotHandler.GetByIdAsync(id);
        return OperationResult<GetLotByIdResponse>.SuccessResult(lot);
    }

    public async Task<OperationResult<string>> CreateAsync(CreateLotRequest model)
    {
        var lot = new Lot
        {
            Id = model.Id,
            Block = new string(model.Id.Where(char.IsLetter).ToArray()),
            Number = Convert.ToInt32(new string(model.Id.Where(char.IsDigit).ToArray())),
            SurveyDate = model.SurveyDate,
            LastModifiedDate = model.LastModifiedDate,
            Status = model.Status,
            Comments = model.Comments,
            User = await userHandler.GetUserByIdAsync(model.UserId),
            Region = await regionHandler.GetRegionByIdAsync(model.RegionId)
        };
        
        var id = await lotHandler.InsertAsync(lot);

        return !string.IsNullOrEmpty(id) ? OperationResult<string>.SuccessResult(id) : OperationResult<string>.FailureResult("Não foi possível inserir o lote!");
    }

    public async Task<OperationResult> UpdateAsync(UpdateLotRequest model)
    {
        var lot = await lotHandler.GetLotById(model.Id);
        if (lot is null)
            return OperationResult.FailureResult($"Lote '{model.Id}' não encontrado!");

        var user = userHandler.GetUserByIdAsync(model.UserId).Result;
        if (user is null)
            return OperationResult.FailureResult($"Usuário '{model.UserId}' não encontrado!");
        
        var region = regionHandler.GetRegionByIdAsync(model.RegionId).Result;
        if (region is null)
            return OperationResult.FailureResult($"Região '{model.RegionId}' não encontrada!");
        
        var clients = clientHandler.GetClientsByIdListAsync(model.Clients).Result;
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
        
        var rowsAffected = await lotHandler.UpdateAsync(lot);

        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Não foi possível alterar o lote!");
    }

    public async Task<OperationResult> DeleteAsync(string id)
    {
        var rowsAffected = await lotHandler.DeleteAsync(id);
        return rowsAffected > 0 ? OperationResult.SuccessResult("Lote removido!") : OperationResult.FailureResult("Não foi possível apagar o lote!");
    }
}