using Promore.Core;
using Promore.Core.Contracts;
using Promore.Core.ViewModels.Responses;

namespace Promore.UseCases.Region.UseCases;

public class GetById(IRegionRepository regionRepository)
{
    public async Task<OperationResult<RegionResponse>> Handle(int id)
    {
        var region = await regionRepository.GetByIdAsync(id);
        if (region is null)
            return OperationResult<RegionResponse>.FailureResult($"Região '{id}' não encontrada!");
        
        return OperationResult<RegionResponse>.SuccessResult(region);
    }
}