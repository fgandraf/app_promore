using Promore.Core;
using Promore.Core.Contracts;
using Promore.Core.ViewModels.Responses;

namespace Promore.UseCases.Region.UseCases;

public class GetAll(IRegionRepository regionRepository)
{
    public async Task<OperationResult<List<RegionResponse>>> Handle()
    {
        var regions = await regionRepository.GetAll();
        if (regions.Count == 0)
            return OperationResult<List<RegionResponse>>.FailureResult("Nenhuma região cadastrada!");
        
        return OperationResult<List<RegionResponse>>.SuccessResult(regions);
    }
}