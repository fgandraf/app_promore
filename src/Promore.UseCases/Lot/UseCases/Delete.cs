using Promore.Core;
using Promore.Core.Contracts;

namespace Promore.UseCases.Lot.UseCases;

public class Delete(ILotRepository lotRepository)
{
    public async Task<OperationResult> Handle(string id)
    {
        var rowsAffected = await lotRepository.DeleteAsync(id);
        return rowsAffected > 0 ? OperationResult.SuccessResult("Lote removido!") : OperationResult.FailureResult("Não foi possível apagar o lote!");
    }
}