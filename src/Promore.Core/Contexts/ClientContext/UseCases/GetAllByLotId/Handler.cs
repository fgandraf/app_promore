using Promore.Core.Contexts.ClientContext.Contracts;

namespace Promore.Core.Contexts.ClientContext.UseCases.GetAllByLotId;


public class Handler(IClientRepository clientRepository)
{
    public async Task<OperationResult<List<Response>>> Handle(string id)
    {
        var clients = await clientRepository.GetAllByLotId(id);
        if (clients.Count == 0)
            return OperationResult<List<Response>>.FailureResult("O lote não possui clientes cadastrados!");
        
        return OperationResult<List<Response>>.SuccessResult(clients);
    }
}