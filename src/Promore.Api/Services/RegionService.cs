using Promore.Core;
using Promore.Core.Handlers;
using Promore.Core.Models;
using Promore.Core.Requests.Regions;
using Promore.Core.Responses.Regions;

namespace Promore.Api.Services;

public class RegionService(IRegionHandler regionHandler, IUserHandler userHandler)
{
    public async Task<OperationResult<List<GetRegionsResponse>>> GetAllAsync()
    {
        var regions = await regionHandler.GetAll();
        if (regions.Count == 0)
            return OperationResult<List<GetRegionsResponse>>.FailureResult("Nenhuma região cadastrada!");
        
        return OperationResult<List<GetRegionsResponse>>.SuccessResult(regions);
    }

    public async Task<OperationResult<GetRegionsByIdResponse>> GetByIdAsync(int id)
    {
        var region = await regionHandler.GetByIdAsync(id);
        if (region is null)
            return OperationResult<GetRegionsByIdResponse>.FailureResult($"Região '{id}' não encontrada!");
        
        return OperationResult<GetRegionsByIdResponse>.SuccessResult(region);
    }

    public async Task<OperationResult<long>> CreateAsync(CreateRegionRequest model)
    {
        var users = userHandler.GetEntitiesByIdsAsync(model.Users).Result;
        
        var region = new Region
        {
            Name = model.Name,
            EstablishedDate = model.EstablishedDate,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            Users = users
        };
        
        var id = await regionHandler.InsertAsync(region);

        return id > 0 ? OperationResult<long>.SuccessResult(id) : OperationResult<long>.FailureResult("Não foi possível inserir a região!");
    }

    public async Task<OperationResult> UpdateAsync(UpdateRegionRequest model)
    {
        var region = await regionHandler.GetRegionByIdAsync(model.Id);
        
        region.Name = model.Name;
        region.EstablishedDate = model.EstablishedDate;
        region.StartDate = model.StartDate;
        region.EndDate = model.EndDate;
        
        var rowsAffected = await regionHandler.UpdateAsync(region);

        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Não foi possível alterar a região!");
    }

    public async Task<OperationResult> DeleteAsync(int id)
    {
        var rowsAffected = await regionHandler.DeleteAsync(id);
        return rowsAffected > 0 ? OperationResult.SuccessResult("Região removida!") : OperationResult.FailureResult("Não foi possível apagar a região!");
    }
}