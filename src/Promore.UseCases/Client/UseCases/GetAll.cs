using Promore.Core;
using Promore.Core.Contracts;
using Promore.Core.ViewModels.Responses;

namespace Promore.UseCases.Client.UseCases;

public class GetAll(IClientRepository clientRepository)
{
    public async Task<OperationResult<List<ClientResponse>>> Handle()
    {
        var clients = await clientRepository.GetAll();
        if (clients.Count == 0)
            return OperationResult<List<ClientResponse>>.FailureResult("Nenhum cliente cadastrado!");
        
        return OperationResult<List<ClientResponse>>.SuccessResult(clients);
    }
    
}