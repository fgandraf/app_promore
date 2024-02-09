using Promore.Core;
using Promore.Core.Contracts;

namespace Promore.UseCases.Region.UseCases;

public class Delete(IRegionRepository regionRepository)
{
    public async Task<OperationResult> Handle(int id)
    {
        var rowsAffected = await regionRepository.DeleteAsync(id);
        return rowsAffected > 0 ? OperationResult.SuccessResult("Região removida!") : OperationResult.FailureResult("Não foi possível apagar a região!");
    }
}