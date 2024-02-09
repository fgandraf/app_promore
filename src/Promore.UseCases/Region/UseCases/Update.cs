using Promore.Core;
using Promore.Core.Contracts;
using Promore.Core.ViewModels.Requests;

namespace Promore.UseCases.Region.UseCases;

public class Update(IRegionRepository regionRepository)
{
    public async Task<OperationResult> Handle(RegionUpdateRequest model)
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