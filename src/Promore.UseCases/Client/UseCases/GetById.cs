using Promore.Core;
using Promore.Core.Contracts;
using Promore.Core.ViewModels.Responses;

namespace Promore.UseCases.Client.UseCases;

public class GetById(IClientRepository clientRepository)
{
    public async Task<OperationResult<ClientResponse>> Handle(int id)
    {
        var client = await clientRepository.GetByIdAsync(id);
        if (client is null)
            return OperationResult<ClientResponse>.FailureResult("Cliente não encontrado!");
        
        return OperationResult<ClientResponse>.SuccessResult(client);
    }
}