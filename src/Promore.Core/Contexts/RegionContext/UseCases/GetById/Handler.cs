using Promore.Core.Contexts.RegionContext.Contracts;

namespace Promore.Core.Contexts.RegionContext.UseCases.GetById;

public class Handler(IRegionRepository regionRepository)
{
    public async Task<OperationResult<Response>> Handle(int id)
    {
        var region = await regionRepository.GetByIdAsync(id);
        if (region is null)
            return OperationResult<Response>.FailureResult($"Região '{id}' não encontrada!");
        
        return OperationResult<Response>.SuccessResult(region);
    }
}