using Promore.Core.Contexts.RegionContext.Contracts;

namespace Promore.Core.Contexts.RegionContext.UseCases.Update;

public class Handler(IRegionRepository regionRepository)
{
    public async Task<OperationResult> Handle(UpdateRegionRequest model)
    {
        var region = await regionRepository.GetRegionByIdAsync(model.Id);
        
        region.Name = model.Name;
        region.EstablishedDate = model.EstablishedDate;
        region.StartDate = model.StartDate;
        region.EndDate = model.EndDate;
        
        var rowsAffected = await regionRepository.UpdateAsync(region);

        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Não foi possível alterar a região!");
    }
}