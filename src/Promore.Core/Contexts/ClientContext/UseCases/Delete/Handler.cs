using Promore.Core.Contexts.ClientContext.Contracts;

namespace Promore.Core.Contexts.ClientContext.UseCases.Delete;

public class Handler(IClientRepository clientRepository)
{
    public async Task<OperationResult> Handle(int id)
    {
        var rowsAffected = await clientRepository.DeleteAsync(id);
        return rowsAffected > 0 ? OperationResult.SuccessResult("Cliente removido!") : OperationResult.FailureResult("Não foi possível apagar o cliente!");

    }
}