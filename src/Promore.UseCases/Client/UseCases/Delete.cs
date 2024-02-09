using Promore.Core;
using Promore.Core.Contracts;

namespace Promore.UseCases.Client.UseCases;

public class Delete(IClientRepository clientRepository)
{
    public async Task<OperationResult> Handle(int id)
    {
        var rowsAffected = await clientRepository.DeleteAsync(id);
        return rowsAffected > 0 ? OperationResult.SuccessResult("Cliente removido!") : OperationResult.FailureResult("Não foi possível apagar o cliente!");

    }
}