using Promore.Core.Contexts.ClientContext.Contracts;

namespace Promore.Core.Contexts.ClientContext.UseCases.GetById;

public class Handler(IClientRepository clientRepository)
{
    public async Task<OperationResult<Response>> Handle(int id)
    {
        var client = await clientRepository.GetByIdAsync(id);
        if (client is null)
            return OperationResult<Response>.FailureResult("Cliente não encontrado!");
        
        return OperationResult<Response>.SuccessResult(client);
    }
}