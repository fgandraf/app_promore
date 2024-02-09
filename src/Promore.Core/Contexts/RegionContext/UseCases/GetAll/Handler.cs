using Promore.Core.Contexts.RegionContext.Contracts;

namespace Promore.Core.Contexts.RegionContext.UseCases.GetAll;

public class Handler(IRegionRepository regionRepository)
{
    public async Task<OperationResult<List<Response>>> Handle()
    {
        var regions = await regionRepository.GetAll();
        if (regions.Count == 0)
            return OperationResult<List<Response>>.FailureResult("Nenhuma região cadastrada!");
        
        return OperationResult<List<Response>>.SuccessResult(regions);
    }
}