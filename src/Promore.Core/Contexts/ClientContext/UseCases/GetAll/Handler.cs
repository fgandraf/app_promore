using Promore.Core.Contexts.ClientContext.Contracts;

namespace Promore.Core.Contexts.ClientContext.UseCases.GetAll;

public class Handler(IClientRepository clientRepository)
{
    public async Task<OperationResult<List<Response>>> Handle()
    {
        var clients = await clientRepository.GetAll();
        if (clients.Count == 0)
            return OperationResult<List<Response>>.FailureResult("Nenhum cliente cadastrado!");
        
        return OperationResult<List<Response>>.SuccessResult(clients);
    }
    
}