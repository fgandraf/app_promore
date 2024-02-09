using Promore.Core;
using Promore.Core.Contracts;
using ClientResponse = Promore.Core.ViewModels.Responses.ClientResponse;
namespace Promore.UseCases.Client.UseCases;


public class GetAllByLotId(IClientRepository clientRepository)
{
    public async Task<OperationResult<List<ClientResponse>>> Handle(string id)
    {
        var clients = await clientRepository.GetAllByLotId(id);
        if (clients.Count == 0)
            return OperationResult<List<ClientResponse>>.FailureResult("O lote não possui clientes cadastrados!");
        
        return OperationResult<List<ClientResponse>>.SuccessResult(clients);
    }
}