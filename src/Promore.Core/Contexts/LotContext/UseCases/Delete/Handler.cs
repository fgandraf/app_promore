using Promore.Core.Contexts.LotContext.Contracts;

namespace Promore.Core.Contexts.LotContext.UseCases.Delete;

public class Handler(ILotRepository lotRepository)
{
    public async Task<OperationResult> Handle(string id)
    {
        var rowsAffected = await lotRepository.DeleteAsync(id);
        return rowsAffected > 0 ? OperationResult.SuccessResult("Lote removido!") : OperationResult.FailureResult("Não foi possível apagar o lote!");
    }
}