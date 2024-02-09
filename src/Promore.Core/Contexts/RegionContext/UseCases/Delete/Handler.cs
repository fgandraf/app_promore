using Promore.Core.Contexts.RegionContext.Contracts;

namespace Promore.Core.Contexts.RegionContext.UseCases.Delete;

public class Handler(IRegionRepository regionRepository)
{
    public async Task<OperationResult> Handle(int id)
    {
        var rowsAffected = await regionRepository.DeleteAsync(id);
        return rowsAffected > 0 ? OperationResult.SuccessResult("Região removida!") : OperationResult.FailureResult("Não foi possível apagar a região!");
    }
}